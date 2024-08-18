namespace Inventory.Api.Domain
{
    public record ProductDto(string Name, string Description, decimal Price, int amount);
}

