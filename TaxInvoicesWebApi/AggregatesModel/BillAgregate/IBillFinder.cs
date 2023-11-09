namespace TaxInvoicesWebApi.AggregatesModel.BillAgregate;
public interface  IBillFinder
{
    Task<IEnumerable<BillResponseModel>> FindAsync(int idCliente, int numeroFactura);
}
