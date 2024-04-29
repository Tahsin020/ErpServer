using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Depots.GetAllDepot;

public sealed record GetAllDepotQuery() : IRequest<Result<List<Depot>>>;
