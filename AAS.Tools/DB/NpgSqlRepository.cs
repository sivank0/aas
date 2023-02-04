using AAS.Tools.DB.Mappers;
using AAS.Tools.Types;
using AAS.Tools.Types.Results;
using Npgsql;
using System.Collections.Concurrent;
using System.Data;
using System.Dynamic;

namespace AAS.Tools.DB;

public class NpgSqlRepository
{
    private string ConnectionString { get; }
    private readonly ConcurrentDictionary<Type, IMapper> _mappers = new ConcurrentDictionary<Type, IMapper>();

    public NpgSqlRepository(string connectionString = null)
    {
        NpgsqlConnection.GlobalTypeMapper.UseJsonNet(new[] { typeof(Jsonb) });
        ConnectionString = connectionString;
    }

    public static (int offset, int limit) NormalizeRange(int page, int pageSize)
    {
        int offset = Math.Max((page - 1) * pageSize, 0);
        int limit = Math.Max(pageSize, 0);

        return (offset, limit);
    }

    protected int Execute(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.Execute(sql, parameters, commandType));
    }

    protected int Execute(string sql, object parameters, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.Execute(sql, parameters, commandType));
    }

    protected int Execute(string sql, ExpandoObject parameters, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.Execute(sql, parameters, commandType));
    }

    protected T Get<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.Get<T>(sql, parameters, commandType));
    }

    protected List<T> GetList<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.GetList<T>(sql, parameters, commandType));
    }

    protected PagedResult<T> GetPageOver<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.GetPageOver<T>(sql, parameters, commandType));
    }

    protected Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Execution(command => command.GetDictionary<TKey, TValue>(sql, parameters, commandType));
    }

    protected void Execution(Action<IDataAccess> command, string connectionString = null)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString ?? ConnectionString))
        {
            connection.Open();

            IDataAccess dataAccess = new DataAccess(connection, mappers: _mappers);
            command(dataAccess);
        }
    }

    protected T Execution<T>(Func<IDataAccess, T> command, string connectionString = null, int commandTimeout = 30)
    {
        NpgsqlConnectionStringBuilder config = new NpgsqlConnectionStringBuilder(connectionString ?? ConnectionString)
        {
            CommandTimeout = commandTimeout
        };

        using (NpgsqlConnection connection = new NpgsqlConnection(config.ConnectionString))
        {
            connection.Open();

            IDataAccess dataAccess = new DataAccess(connection, mappers: _mappers);
            return command(dataAccess);
        }
    }

    protected void ExecutionInTransaction(Action<IDataAccess> command, string connectionString = null)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString ?? ConnectionString))
        {
            connection.Open();

            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    IDataAccess dataAccess = new DataAccess(connection, transaction, _mappers);
                    command(dataAccess);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }
    }

    protected T ExecutionInTransaction<T>(Func<IDataAccess, T> command, string connectionString = null)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString ?? ConnectionString))
        {
            connection.Open();

            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    IDataAccess dataAccess = new DataAccess(connection, transaction, _mappers);
                    T value = command(dataAccess);
                    transaction.Commit();
                    return value;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }
    }
}