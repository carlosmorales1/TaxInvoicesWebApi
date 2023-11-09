using System.Data;

namespace TaxInvoicesWebApi.ConnectionSql;

public interface IDatabaseConnection
{
    IDbConnection Connection { get; }
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null);
    Task<int> ExecuteAsync(string sql, object parameters = null);
    Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null);
}
