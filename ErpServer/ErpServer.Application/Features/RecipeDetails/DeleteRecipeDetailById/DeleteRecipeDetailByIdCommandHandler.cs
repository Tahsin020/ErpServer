using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.RecipeDetails.DeleteRecipeDetailById;

internal sealed class DeleteRecipeDetailByIdCommandHandler(IRecipeDetailRepository recipeDetailRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRecipeDetailByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteRecipeDetailByIdCommand request, CancellationToken cancellationToken)
    {
        RecipeDetail? recipeDetail = await recipeDetailRepository.GetByExpressionAsync(r => r.Id == request.Id, cancellationToken);

        if (recipeDetail is null)
        {
            return Result<string>.Failure("Reçetede bu ürün bulunamadı.");
        }

        recipeDetailRepository.Delete(recipeDetail);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Reçetede ürün bilgisi başarıyla silindi";
    }
}
