using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Recipes.DeleteRecipeById;

public sealed record DeleteRecipeByIdCommand(Guid Id) : IRequest<Result<string>>;


internal sealed class DeleteRecipeByIdCommandHandler(IRecipeRepository recipeRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRecipeByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRecipeByIdCommand request, CancellationToken cancellationToken)
    {
        Recipe? recipe = await recipeRepository.GetByExpressionAsync(r => r.Id == request.Id, cancellationToken);
        if (recipe is null)
        {
            return Result<string>.Failure("Reçete bilgisi bulunamadı.");
        }

        recipeRepository.Delete(recipe);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçete bilgisi başarıyla silinmiştir.";
    }
}
