using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Api.Services;

namespace Product.Api.Controllers
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

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAll()
        { 
            var result = await  _products.GetAllAsync();
            if(result.IsSuccess)
                return Ok(result);
            return NotFound(result.ErrorMessage);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            var result = await _products.GetByIdAsync(Id);
            if (result.IsSuccess)
                return Ok(result);
            return NotFound(result.ErrorMessage);
        }
    }
}
