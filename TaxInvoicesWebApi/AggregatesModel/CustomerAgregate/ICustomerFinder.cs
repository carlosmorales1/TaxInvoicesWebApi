namespace TaxInvoicesWebApi.AggregatesModel.CustomerAgregate;
public interface  ICustomerFinder
{
    Task<IEnumerable<CustomerModel>> FindAsync();
}
