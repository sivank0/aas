using AAS.Tools.Types.Results;
using System.Data;
using System.Dynamic;

namespace AAS.Tools.DB;

public interface IDataAccess : IDisposable
{
    int Execute(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text);
    int Execute(string sql, object parameters, CommandType commandType = CommandType.Text);
    int Execute(string sql, ExpandoObject parameters, CommandType commandType = CommandType.Text);
    T Get<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text);

    List<T> GetList<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text);
    PagedResult<T> GetPageOver<T>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text);
    Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(string sql, IList<SqlParameter> parameters = null, CommandType commandType = CommandType.Text);
}
