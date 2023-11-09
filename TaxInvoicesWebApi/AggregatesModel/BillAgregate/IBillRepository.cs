namespace TaxInvoicesWebApi.AggregatesModel.BillAgregate;
public interface  IBillRepository
{
    Task<int> CreateBill(BillModel data);
    Task<int> CreateBillDetail(List<BillDetailModel> data);
}

