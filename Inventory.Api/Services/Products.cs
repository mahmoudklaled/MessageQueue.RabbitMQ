using Common.Models;
using Common.Result;
using Microsoft.EntityFrameworkCore;
using Inventory.Api.Database;
using Inventory.Api.Domain;
using System.Collections.Immutable;

namespace Inventory.Api.Services
{
    public class Products : IProducts
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public Products(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<TResult<Product>> AddAsync(ProductDto productDto)
        {
            Product product = MapProduct(productDto);
            try
            {
                await _applicationDbContext.Products.AddAsync(product);

                var result = await _applicationDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    return new TResult<Product>
                    {
                        IsSuccess = true,
                        result = product,
                        ErrorMessage = "Product added successfully."
                    };
                }
                else
                {
                    return new TResult<Product>
                    {
                        IsSuccess = false,
                        result = null,
                        ErrorMessage = "No changes were saved."
                    };
                }
            }
            catch (Exception ex)
            {
                return new TResult<Product>
                {
                    IsSuccess = false,
                    result = null,
                    ErrorMessage = $"An error occurred: {ex.Message}"
                };
            }
        }

        private Product MapProduct(ProductDto productDto)
        {
            return new Product
            {
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Amount = productDto.amount,
                Description = productDto.Description,
                Price = productDto.Price
            };
        }
    }
}
