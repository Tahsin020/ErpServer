using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Products.GetAllProduct;

public sealed record GetAllProductQuery() : IRequest<Result<List<GetAllProductQueryResponse>>>;
