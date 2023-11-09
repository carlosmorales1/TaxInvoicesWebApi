using Microsoft.AspNetCore.Mvc;
using TaxInvoicesWebApi.AggregatesModel.CustomerAgregate;

namespace TaxInvoicesWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{

    private readonly ICustomerFinder _customerFinder;

    public CustomerController(ICustomerFinder customerFinder)
    {
        _customerFinder = customerFinder;
    }

    
    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        try{
            var res = await _customerFinder.FindAsync();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}