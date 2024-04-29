using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ErpServer.Application.Features.Invoices.DeleteInvoiceById;

internal sealed class DeleteInvoiceByIdCommandHandler(IInvoiceRepository invoiceRepository,IStockMovementRepository stockMovementRepository ,IUnitOfWork unitOfWork) : IRequestHandler<DeleteInvoiceByIdCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteInvoiceByIdCommand request, CancellationToken cancellationToken)
    {
        Invoice? invoice = 
            await invoiceRepository.GetByExpressionAsync(i => i.Id == request.Id, cancellationToken);

        if (invoice is null)
        {
           return Result<string>.Failure("Fatura bilgisi bulunamadı");
        }

        List<StockMovement> movements =
            await stockMovementRepository
            .Where(i => i.InvoiceId == invoice.Id).ToListAsync(cancellationToken);

        stockMovementRepository.DeleteRange(movements);

        invoiceRepository.Delete(invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla silindi";
    }
}
