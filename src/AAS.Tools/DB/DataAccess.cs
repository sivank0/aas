#region

using System.Collections.Concurrent;
using System.Data;
using System.Dynamic;
using System.Reflection;
using AAS.Tools.DB.Common;
using AAS.Tools.DB.Enums;
using AAS.Tools.DB.Mappers;
using AAS.Tools.Extensions;
using AAS.Tools.Types.Results;

#endregion

namespace AAS.Tools.DB;

public class DataAccess : IDataAccess
{
    private ConcurrentDictionary<Type, IMapper> Mappers { get; }
    private IDbConnection Connection { get; }
    private IDbTransaction Transaction { get; }


    internal DataAccess(IDbConnection connection, IDbTransaction transaction = null,
        ConcurrentDictionary<Type, IMapper> mappers = null)
    {
        Mappers = mappers ?? new ConcurrentDictionary<Type, IMapper>();
        Connection = connection;
        Transaction = transaction;
    }

    private IMapper GetAndSetMapperType(Type type)
    {
        if (Mappers.TryGetValue(type, out IMapper mapperType)) return mapperType;

        if (ReflectionHelper.IsSimpleType(type))
            mapperType = new SimpleTypeMapper(type);
        else
            mapperType = new ClassMapper(type);

        Mappers[type] = mapperType;

        return mapperType;
    }

    public int Execute(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();
            return command.ExecuteNonQuery();
        }, Transaction);
    }

    public int Execute(string sql, object parameters, CommandType commandType = CommandType.Text)
    {
        List<SqlParameter> sqlParameters = new();
        foreach (PropertyInfo property in parameters.GetType().GetProperties())
            sqlParameters.Add(new SqlParameter($"p_{property.Name.ToCamelCase()}", property.GetValue(parameters)));

        return Execute(sql, sqlParameters, commandType);
    }

    public int Execute(string sql, ExpandoObject parameters, CommandType commandType = CommandType.Text)
    {
        SqlParameter[] sqlParameters =
            parameters.Select(p => new SqlParameter($"p_{p.Key.ToCamelCase()}", p.Value)).ToArray();

        return Execute(sql, sqlParameters, commandType);
    }

    public T Get<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text)
    {
        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                IMapper mapper = GetAndSetMapperType(typeof(T));
                return dataReader.GetValue<T>(mapper);
            }
        }, Transaction);
    }

    public List<T> GetList<T>(string sql, IList<SqlParameter> parameters = null,
        CommandType commandType = CommandType.Text)
    {
        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();

            using (IDataReader dataReader = command.ExecuteReader())
            {
                IMapper mapper = GetAndSetMapperType(typeof(T));
                return dataReader.GetList<T>(mapper);
            }
        }, Transaction);
    }

    public PagedResult<T> GetPageOver<T>(string sql, IList<SqlParameter> parameters = null,
        CommandType commandType = CommandType.Text)
    {
        IMapper dataType = GetAndSetMapperType(typeof(T));

        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();

            using (IDataReader dr = command.ExecuteReader())
            {
                List<T> values = dr.GetList<T>(dataType, out long totalRows);
                return new PagedResult<T>(values, totalRows);
            }
        });
    }

    public PagedResult<T> GetPage<T>(string sql, IList<SqlParameter> parameters = null,
        CommandType commandType = CommandType.Text)
    {
        IMapper dataType = GetAndSetMapperType(typeof(T));
        IMapper dataTypeInt = GetAndSetMapperType(typeof(long));

        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();

            using (IDataReader dr = command.ExecuteReader())
            {
                long totalRows = dr.GetValue<long>(dataTypeInt);

                dr.NextResult();

                return new PagedResult<T>(dr.GetList<T>(dataType), totalRows);
            }
        });
    }

    public Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, IList<SqlParameter> parameters = null,
        CommandType commandType = CommandType.Text)
    {
        return Connection.ExecuteCommand(command =>
        {
            if (parameters != null)
                command.AddParameters(parameters);

            command.CommandType = commandType;
            command.CommandText = sql;
            command.Prepare();

            using (IDataReader dr = command.ExecuteReader())
            {
                return dr.GetDictionary<TKey, TValue>();
            }
        });
    }


    private void ExecuteNonQuery(IDbCommand command)
    {
        command.Prepare();
        command.ExecuteNonQuery();
        command.Parameters.Clear();
    }

    public void Add<T>(T value) where T : new()
    {
        if (value == null) throw new ArgumentNullException(nameof(value), "value is null");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.AddParametersForInsert(value, mapper);
            command.CommandText = mapper.CreateInsertCommand();
            command.Prepare();
            command.ExecuteNonQuery();
        }, Transaction);
    }

    public void Add<T>(IList<T> values) where T : new()
    {
        if (values == null) throw new ArgumentNullException(nameof(values), "value is null");
        if (values.Count == 0) return;
        if (values.Any(value => value == null))
            throw new ArgumentNullException(nameof(values), "values contains null value");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.CommandText = mapper.CreateInsertCommand();

            foreach (T value in values)
            {
                command.AddParametersForInsert(value, mapper);
                ExecuteNonQuery(command);
            }
        }, Transaction);
    }

    public void Edit<T>(T value) where T : new()
    {
        if (value == null) throw new ArgumentNullException(nameof(value), "value is null");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.AddParametersForUpdate(value, mapper);
            command.CommandText = mapper.CreateUpdateCommand();
            command.Prepare();
            command.ExecuteNonQuery();
        }, Transaction);
    }

    public void Edit<T>(IList<T> values) where T : new()
    {
        if (values == null) throw new ArgumentNullException(nameof(values), "value is null");
        if (values.Count == 0) return;
        if (values.Any(value => value == null))
            throw new ArgumentNullException(nameof(values), "values contains null value");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.CommandText = mapper.CreateUpdateCommand();

            foreach (T value in values)
            {
                command.AddParametersForUpdate(value, mapper);
                ExecuteNonQuery(command);
            }
        }, Transaction);
    }

    public void Save<T>(Dictionary<DataCommandType, T[]> values) where T : new()
    {
        if (values == null) throw new ArgumentNullException(nameof(values), "value is null");
        if (values.Count == 0) return;

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            if (values.ContainsKey(DataCommandType.Add))
            {
                command.CommandText = mapper.CreateInsertCommand();

                foreach (T value in values[DataCommandType.Add])
                {
                    command.AddParametersForInsert(value, mapper);
                    ExecuteNonQuery(command);
                }
            }

            if (values.ContainsKey(DataCommandType.Edit))
            {
                command.CommandText = mapper.CreateUpdateCommand();

                foreach (T value in values[DataCommandType.Edit])
                {
                    command.AddParametersForUpdate(value, mapper);
                    ExecuteNonQuery(command);
                }
            }

            if (values.ContainsKey(DataCommandType.RemoveByUpdate))
            {
                command.CommandText = mapper.CreateRemoveByUpdateCommand();

                foreach (T value in values[DataCommandType.RemoveByUpdate])
                {
                    command.AddParametersForRemoveByUpdate(value, mapper);
                    ExecuteNonQuery(command);
                }
            }

            if (values.ContainsKey(DataCommandType.Remove))
            {
                command.CommandText = mapper.CreateRemoveCommand();

                foreach (T value in values[DataCommandType.Remove])
                {
                    command.AddParametersForRemove(value, mapper);
                    ExecuteNonQuery(command);
                }
            }
        }, Transaction);
    }

    public void RemoveByUpdate<T>(T value) where T : new()
    {
        if (value == null) throw new ArgumentNullException(nameof(value), "value is null");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.AddParametersForRemoveByUpdate(value, mapper);
            command.CommandText = mapper.CreateRemoveByUpdateCommand();
            command.Prepare();
            command.ExecuteNonQuery();
        }, Transaction);
    }

    public void RemoveByUpdate<T>(IList<T> values) where T : new()
    {
        if (values == null) throw new ArgumentNullException(nameof(values), "values is null");
        if (values.Count == 0) return;
        if (values.Any(value => value == null))
            throw new ArgumentNullException(nameof(values), "values contains null value");

        ClassMapper mapper = GetAndSetMapperType(typeof(T)) as ClassMapper;

        Connection.ExecuteCommand(command =>
        {
            command.CommandText = mapper.CreateRemoveByUpdateCommand();

            foreach (T value in values)
            {
                command.AddParametersForRemoveByUpdate(value, mapper);
                ExecuteNonQuery(command);
            }
        }, Transaction);
    }

    public void Dispose()
    {
        Connection?.Dispose();
        Transaction?.Dispose();
        GC.SuppressFinalize(this);
    }
}