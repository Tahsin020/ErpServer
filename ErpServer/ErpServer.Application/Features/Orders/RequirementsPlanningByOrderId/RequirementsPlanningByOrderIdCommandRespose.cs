using ErpServer.Domain.Dtos;

namespace ErpServer.Application.Features.Orders.RequirementsPlanningByOrderId;

public sealed record RequirementsPlanningByOrderIdCommandRespose(
    DateOnly Date,
    string title,
    List<ProductDto> Products);
