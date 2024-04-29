using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Orders.DeleteOrderById;

public sealed record DeleteOrderByIdCommand(Guid Id) : IRequest<Result<string>>;
