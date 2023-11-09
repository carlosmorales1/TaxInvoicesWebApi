using Org.BouncyCastle.Crypto;

namespace TaxInvoicesWebApi.AggregatesModel.ProductAgregate;
public interface  IProductFinder
{
    Task<IEnumerable<ProductModel>> FindAsync();
    Task<IEnumerable<ProductModel>> FindAsyncByIds(List<int> ids);
}
