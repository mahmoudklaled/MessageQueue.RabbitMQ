namespace Product.Api.Domain
{
    public record ProductDto(Guid Id, string Name, decimal Price, int amount);
}
