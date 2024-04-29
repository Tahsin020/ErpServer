using ErpServer.Domain.Abstractions;

namespace ErpServer.Domain.Entities;

public sealed class OrderDetail : Entity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}