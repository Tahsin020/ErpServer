using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Depots.DeleteDepotById;

public sealed record DeleteDepotByIdCommand(Guid Id) : IRequest<Result<string>>;

internal sealed class DeleteDepotByIdCommandHandler(IDepotRepository depotRepository,IUnitOfWork unitOfWork) : IRequestHandler<DeleteDepotByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteDepotByIdCommand request, CancellationToken cancellationToken)
    {
        Depot? depot = await depotRepository.GetByExpressionAsync(p => p.Id == request.Id, cancellationToken);

        if (depot is null)
        {
            return Result<string>.Failure("Depo bulunamadı");
        }

        depotRepository.Delete(depot);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Depo kaydı başarıyla silindi";
    }
}
