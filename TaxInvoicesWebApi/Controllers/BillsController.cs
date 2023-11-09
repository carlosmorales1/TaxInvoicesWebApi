using Microsoft.AspNetCore.Mvc;
using TaxInvoicesWebApi.AggregatesModel.BillAgregate;
using TaxInvoicesWebApi.AggregatesModel.ProductAgregate;

namespace TaxInvoicesWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BillsController : ControllerBase
{

    private readonly IBillFinder _billFinder;
    private readonly IBillRepository _billRepository;
    private readonly IProductFinder _productFinder;
    public BillsController(IBillFinder billFinder, IBillRepository billRepository, IProductFinder productFinder)
    {
        _billFinder = billFinder; 
        _billRepository = billRepository; 
        _productFinder = productFinder;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetSearchInvoices([FromQuery] int cliente, [FromQuery] int numero)
    {
        try{
            var res = await _billFinder.FindAsync(cliente, numero);
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] InvoiceNewModel data)
    {
        try{
            decimal iva = 0.19m;
            decimal subTotalFactura = 0;
            int totalNumberItems = 0;

            var productsDetail = await _productFinder.FindAsyncByIds(data.Products.Select(product => product.ProductId).ToList());

            foreach (var productItem in data.Products)
            {
                var productDetail = productsDetail.FirstOrDefault(product => product.Id == productItem.ProductId);
                if(productDetail != null){
                    subTotalFactura += productDetail.PrecioUnitario * productItem.Quantity;
                    totalNumberItems += productItem.Quantity;
                }
            }
            decimal totalImpuesto = subTotalFactura * iva;

            BillModel bill = new BillModel{
                FechaEmisionFactura = DateTime.Now,
                IdCliente = data.CustomerId,
                NumeroFactura = data.InvoiceNumber,
                NumeroTotalArticulos = totalNumberItems,
                SubTotalFactura = subTotalFactura,
                TotalImpuesto = totalImpuesto,
                TotalFactura = subTotalFactura + totalImpuesto,
            };

            var idBill = await _billRepository.CreateBill(bill);

            List<BillDetailModel> billsDetail = new List<BillDetailModel>();
            foreach (var productItem in data.Products){
                decimal UnitPriceProduct = 0;
                string productName = "None";
                var productDetail = productsDetail.FirstOrDefault(product => product.Id == productItem.ProductId);
                if(productDetail != null){
                    UnitPriceProduct = productDetail.PrecioUnitario;
                    productName = productDetail.NombreProducto;
                }
                billsDetail.Add(
                    new BillDetailModel{
                        IdFactura = idBill,
                        IdProducto = productItem.ProductId,
                        CantidadDeProducto = productItem.Quantity,
                        PrecioUnitarioProducto = UnitPriceProduct,
                        SubTotalProducto = UnitPriceProduct * productItem.Quantity,
                        Notas = productName
                    }
                );
            }
            await _billRepository.CreateBillDetail(billsDetail);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}