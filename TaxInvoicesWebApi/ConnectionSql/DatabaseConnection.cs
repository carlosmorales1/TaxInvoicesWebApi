using System.Data.SqlClient;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;

namespace TaxInvoicesWebApi.ConnectionSql;

public class DatabaseConnection : IDatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
        Connection = new MySqlConnection(_connectionString);
    }

    public IDbConnection Connection { get; }

    public Task OpenAsync()
    {
        return Task.Run(() => Connection.Open());
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null)
    {
        using (var connection = Connection)
        {
            await OpenAsync();

            return await connection.QueryAsync<T>(sql, parameters);
        }
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null)
    {
        using (var connection = Connection)
        {
            await OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }
    }

    public async Task<int> ExecuteAsync(string sql, object parameters = null)
    {
        using (var connection = Connection)
        {
            await OpenAsync();

            return await connection.ExecuteAsync(sql, parameters);
        }
    }
}
