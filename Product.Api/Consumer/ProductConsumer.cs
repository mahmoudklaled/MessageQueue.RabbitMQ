using MassTransit;
using Product.Api.Services;

namespace Product.Api.Consumer
{
    public sealed class ProductConsumer : IConsumer<Common.Models.Product>
    {
        private readonly IProducts _products;

        public ProductConsumer(IProducts products)
        {
            _products = products;
        }

        public async Task Consume(ConsumeContext<Common.Models.Product> context)
        {
            await _products.AddAsync(context.Message);
        }
    }
}
