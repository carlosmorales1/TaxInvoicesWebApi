namespace TaxInvoicesWebApi.AggregatesModel.BillAgregate;
public class BillModel
{
    public DateTime FechaEmisionFactura { get; set; }
    public int? IdCliente { get; set; }
    public int? NumeroFactura { get; set; }
    public int? NumeroTotalArticulos { get; set; }
    public decimal? SubTotalFactura { get; set; }
    public decimal? TotalImpuesto { get; set; }
    public decimal? TotalFactura { get; set; }
}

public class BillIdModel
{
    public int Id { get; set; }
}

public class BillDetailModel
{
    public int IdFactura { get; set; }
    public int IdProducto { get; set; }
    public int CantidadDeProducto { get; set; }
    public decimal PrecioUnitarioProducto { get; set; }
    public decimal SubTotalProducto { get; set; }
    public string Notas { get; set; }
}

public class BillResponseModel
{
    public DateTime FechaEmisionFactura { get; set; }
    public int? NumeroFactura { get; set; }
    public decimal? TotalFactura { get; set; }
}

public class ProductNewModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class InvoiceNewModel
{
    public List<ProductNewModel> Products { get; set; }
    public int CustomerId { get; set; }
    public int InvoiceNumber { get; set; }
}