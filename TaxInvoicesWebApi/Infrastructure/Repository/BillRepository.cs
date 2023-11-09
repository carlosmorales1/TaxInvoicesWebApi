using TaxInvoicesWebApi.AggregatesModel.BillAgregate;
using TaxInvoicesWebApi.ConnectionSql;

namespace TaxInvoicesWebApi.Infrastructure.Repository;
public class BillRepository: IBillRepository
{
    private readonly IDatabaseConnection _connection;
    public BillRepository(IDatabaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> CreateBill(BillModel data){
        string sql = @"INSERT INTO tblfacturas (FechaEmisionFactura, IdCliente, NumeroFactura, 
                    NumeroTotalArticulos, SubTotalFactura, TotalImpuesto, TotalFactura) 
                    VALUES (@FechaEmisionFactura, @IdCliente, @NumeroFactura, 
                    @NumeroTotalArticulos, @SubTotalFactura, @TotalImpuesto, @TotalFactura)"; //Pasar los comandos SQL a un archivo independiente
        
        await _connection.ExecuteAsync(sql, data);


        string checkLastIdSql = "SELECT Id FROM tblfacturas WHERE NumeroFactura = @NumeroFactura ORDER BY Id DESC LIMIT 1";

        var res = await _connection.QueryFirstOrDefaultAsync<BillIdModel>(checkLastIdSql, new {
            data.NumeroFactura
        });

        return res.Id;
    }

    public async Task<int> CreateBillDetail(List<BillDetailModel> data){
        string sql = @"INSERT INTO tbldetallesfacturas (IdFactura, IdProducto, CantidadDeProducto, PrecioUnitarioProducto, SubTotalProducto, Notas) 
                       VALUES (@IdFactura, @IdProducto, @CantidadDeProducto, @PrecioUnitarioProducto, @SubTotalProducto, @Notas)"; //Pasar los comandos SQL a un archivo independiente
        
        var res = await _connection.ExecuteAsync(sql, data);

        return res;
    }

    
}
