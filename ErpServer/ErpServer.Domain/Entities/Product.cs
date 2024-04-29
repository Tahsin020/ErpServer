using ErpServer.Domain.Abstractions;
using ErpServer.Domain.Enums;

namespace ErpServer.Domain.Entities;

public sealed class Product : Entity
{
    public string Name { get; set; } = string.Empty;
    public ProductTypeEnum Type { get; set; } = ProductTypeEnum.Product;
}
