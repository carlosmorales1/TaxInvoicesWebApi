using System.Data;
using TaxInvoicesWebApi.AggregatesModel.CustomerAgregate;
using TaxInvoicesWebApi.ConnectionSql;

namespace TaxInvoicesWebApi.Infrastructure.Finder;
public class CustomerFinder: ICustomerFinder
{
    private readonly IDatabaseConnection _connection;
    public CustomerFinder(IDatabaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<CustomerModel>> FindAsync(){
        var sql = "SELECT Id, RazonSocial FROM tblclientes"; //Pasar los comandos SQL a un archivo independiente
        return await _connection.QueryAsync<CustomerModel>(sql);
    }
}
