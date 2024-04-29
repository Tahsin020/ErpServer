using ErpServer.Domain.Enums;

namespace ErpServer.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQueryResponse
{
    public string Name { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public ProductTypeEnum Type { get; set; } = ProductTypeEnum.Product;
    public decimal Stock { get; set; }
} 
