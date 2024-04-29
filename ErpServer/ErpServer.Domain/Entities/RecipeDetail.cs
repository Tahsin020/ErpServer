using ErpServer.Domain.Abstractions;

namespace ErpServer.Domain.Entities;

public sealed class RecipeDetail : Entity
{
    public Guid RecipeId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }

}
