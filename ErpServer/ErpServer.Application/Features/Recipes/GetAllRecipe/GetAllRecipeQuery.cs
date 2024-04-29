using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Recipes.GetAllRecipe;

public sealed record GetAllRecipeQuery() : IRequest<Result<List<Recipe>>>;
