using AutoMapper;
using ErpServer.Domain.Entities;
using ErpServer.Domain.Enums;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Invoices.CreateInvoice;

internal sealed class CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, IStockMovementRepository stockMovementRepository, IUnitOfWork unitOfWork, IMapper mapper,IOrderRepository orderRepository) : IRequestHandler<CreateInvoiceCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Invoice invoice = mapper.Map<Invoice>(request);

        if (invoice.Details is not null)
        {
            List<StockMovement> movements = new();
            foreach (var item in invoice.Details)
            {
                StockMovement movement = new()
                {
                    InvoiceId = invoice.Id, 
                    NumberOfEntries = request.TypeValue == 1 ? item.Quantity : 0,
                    NumberOfOutputs = request.TypeValue == 2 ? item.Quantity : 0,
                    DepotId = item.DepotId,
                    Price = item.Price,
                    ProductId = item.ProductId
                };
                movements.Add(movement);
            }
            await stockMovementRepository.AddRangeAsync(movements, cancellationToken);
        }

        await invoiceRepository.AddAsync(invoice, cancellationToken);

        if (request.OrderId is not null)
        {
            Order order = await orderRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.OrderId,cancellationToken);
            if (order is not null)
            {
                order.Status = OrderStatusEnum.Completed;
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Fatura başarıyla kaydedildi.";
    }
}
