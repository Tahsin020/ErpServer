using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Orders.RequirementsPlanningByOrderId;

public sealed record RequirementsPlanningByOrderIdCommand(Guid OrderId) : IRequest<Result<RequirementsPlanningByOrderIdCommandRespose>>;
