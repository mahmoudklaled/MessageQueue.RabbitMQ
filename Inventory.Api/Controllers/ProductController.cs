using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Inventory.Api.Services;
using Common.Models;
using Inventory.Api.Domain;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProducts _products;

        public ProductController(IProducts products)
        {
            _products = products;
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto product)
        { 
            // validate user data
            var result = await  _products.AddAsync(product);
            if(result.IsSuccess)
                return Ok(result);
            return NotFound(result.ErrorMessage);
        }

       
    }
}
