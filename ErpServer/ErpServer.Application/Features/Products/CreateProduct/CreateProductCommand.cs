using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, int TypeValue) : IRequest<Result<string>>;
