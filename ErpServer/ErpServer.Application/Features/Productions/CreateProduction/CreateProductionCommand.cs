using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Productions.CreateProduction;

public sealed record CreateProductionCommand(
    Guid ProductId,
    Guid DepotId,
    decimal Quantity) : IRequest<Result<string>>;
