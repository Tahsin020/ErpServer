using ErpServer.Domain.Dtos;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Recipes.CreateRecipe;

public sealed record CreateRecipeCommand(
    Guid ProductId,
    List<RecipeDetailDto> Details) : IRequest<Result<string>>;
