using Common.Result;
using Product.Api.Domain;

namespace Product.Api.Services
{
    public interface IProducts
    {
        Task<TResult<IList<ProductDto>>> GetAllAsync();

        Task<TResult<ProductDto>> GetByIdAsync(Guid Id);
    }
}
