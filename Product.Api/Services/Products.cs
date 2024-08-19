using Common.Models;
using Common.Result;
using Microsoft.EntityFrameworkCore;
using Product.Api.Database;
using Product.Api.Domain;
using System.Collections.Immutable;

namespace Product.Api.Services
{
    public class Products : IProducts
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Products(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Common.Models.Product product)
        {
            try
            {
                await _applicationDbContext.Products.AddAsync(product);

                var result = await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //ignore
            }
        }


        public async Task<TResult<IList<ProductDto>>> GetAllAsync()
        {
            var products = await _applicationDbContext.Products.ToListAsync();
            if (products == null)
                return new TResult<IList<ProductDto>>
                {
                    result = new List<ProductDto>(),
                    IsSuccess = true,
                };
            IList<ProductDto> productDto = new List<ProductDto>();

            foreach (var product in products)
            {
                productDto.Add(MapProduct(product));
            }

            return new TResult<IList<ProductDto>>
            {
                result = productDto,
                IsSuccess = true,
            };
        }

        public async Task<TResult<ProductDto>> GetByIdAsync(Guid Id)
        {
            var product = _applicationDbContext.Products.Where(p=> p.Id == Id).FirstOrDefault();
            if (product == null)
                return new TResult<ProductDto>
                {
                    IsSuccess = false,
                    ErrorMessage = $"No Product Match this id : {Id}"
                };
            return new TResult<ProductDto>
            {
                result = MapProduct(product),
                IsSuccess = true,
            };
        }

        #region Helpers
        private ProductDto MapProduct(Common.Models.Product product) => 
            new ProductDto(product.Id , product.Name , product.Price , product.Amount);
        
        #endregion
    }
}
