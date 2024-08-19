using Common.Models;
using Common.Result;
using Microsoft.EntityFrameworkCore;
using Inventory.Api.Database;
using Inventory.Api.Domain;
using System.Collections.Immutable;
using MassTransit;
using MassTransit.Transports;

namespace Inventory.Api.Services
{
    public class Products : IProducts
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IPublishEndpoint _publishEndpoint;
        public Products(ApplicationDbContext applicationDbContext, IPublishEndpoint publishEndpoint)
        {
            _applicationDbContext = applicationDbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<TResult<Product>> AddAsync(ProductDto productDto)
        {
            Product product = MapProduct(productDto);
            try
            {
                await _applicationDbContext.ProductsTbl.AddAsync(product);

                var result = await _applicationDbContext.SaveChangesAsync();

                if (result > 0)
                {
                    await _publishEndpoint.Publish(product);
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
                        ErrorMessage = "No changes were saved."
                    };
                }
            }
            catch (Exception ex)
            {
                return new TResult<Product>
                {
                    IsSuccess = false,
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
