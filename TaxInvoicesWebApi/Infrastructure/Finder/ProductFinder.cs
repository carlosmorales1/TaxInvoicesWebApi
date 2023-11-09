using TaxInvoicesWebApi.AggregatesModel.ProductAgregate;
using TaxInvoicesWebApi.ConnectionSql;

namespace TaxInvoicesWebApi.Infrastructure.Finder;
public class ProductFinder: IProductFinder
{
    private readonly IDatabaseConnection _connection;
    public ProductFinder(IDatabaseConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<ProductModel>> FindAsync(){
        var sql = "SELECT * FROM catproductos";
        return await _connection.QueryAsync<ProductModel>(sql);
    }

    public async Task<IEnumerable<ProductModel>> FindAsyncByIds(List<int> ids){
        var sql = $"SELECT * FROM catproductos WHERE Id IN ({string.Join(",", ids)})"; //Pasar los comandos SQL a un archivo independiente.
        return await _connection.QueryAsync<ProductModel>(sql);
    }
}
