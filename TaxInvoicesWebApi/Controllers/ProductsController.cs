using Microsoft.AspNetCore.Mvc;
using TaxInvoicesWebApi.AggregatesModel.ProductAgregate;

namespace TaxInvoicesWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{

    private readonly IProductFinder _productFinder;

    public ProductsController(IProductFinder productFinder)
    {
        _productFinder = productFinder;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            var res = await _productFinder.FindAsync();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}