using TaxInvoicesWebApi.AggregatesModel.BillAgregate;
using TaxInvoicesWebApi.ConnectionSql;

namespace TaxInvoicesWebApi.Infrastructure.Finder;
public class BillFinder: IBillFinder
{
    private readonly IDatabaseConnection _connection;
    public BillFinder(IDatabaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<BillResponseModel>> FindAsync(int idCliente, int numeroFactura){
        var sql = "SELECT NumeroFactura, FechaEmisionFactura, TotalFactura FROM tblfacturas "; //Pasar los comandos SQL a un archivo independiente.
        if(idCliente != 0){
            sql += $"WHERE IdCliente = {idCliente}";
        } else 
        if(numeroFactura != 0){
            sql += $"WHERE NumeroFactura = {numeroFactura}";
        }
        return await _connection.QueryAsync<BillResponseModel>(sql);
    }
}
