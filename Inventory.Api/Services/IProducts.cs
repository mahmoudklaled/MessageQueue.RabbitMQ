using Common.Models;
using Common.Result;
using Inventory.Api.Domain;

namespace Inventory.Api.Services
{
    public interface IProducts
    {
        Task<TResult<Product>> AddAsync(ProductDto productDto);
    }
}
