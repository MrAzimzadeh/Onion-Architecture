using Eccomerce.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{

    [HttpGet]
    public IActionResult GetProducts()
    {
        return Ok();
    }
    
}