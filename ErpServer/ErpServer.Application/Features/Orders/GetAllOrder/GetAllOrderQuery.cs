using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Orders.GetAllOrder;

public sealed record GetAllOrderQuery() : IRequest<Result<List<Order>>>;
