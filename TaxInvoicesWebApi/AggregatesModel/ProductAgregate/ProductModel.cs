namespace TaxInvoicesWebApi.AggregatesModel.ProductAgregate;
public class ProductModel
{
    public int Id { get; set; }
    public string NombreProducto { get; set; }
    public string ImagenProducto { get; set; }
    public decimal PrecioUnitario { get; set; }
    public string Ext { get; set; }
}