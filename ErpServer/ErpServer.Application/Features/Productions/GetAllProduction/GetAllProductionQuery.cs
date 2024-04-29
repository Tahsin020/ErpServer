using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Productions.GetAllProductions;

public sealed class GetAllProductionQuery() : IRequest<Result<List<Production>>>;
