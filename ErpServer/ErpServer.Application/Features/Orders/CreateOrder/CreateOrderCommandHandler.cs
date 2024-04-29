using AutoMapper;
using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ErpServer.Application.Features.Orders.CreateOrder;

internal sealed class CreateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateOrderCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order? lastOrder =
            await orderRepository
            .Where(P => P.OrderNumberYear == request.Date.Year)
            .OrderByDescending(p => p.OrderNumber)
            .FirstOrDefaultAsync(cancellationToken);

        int lastOrderNumber = 0;

        if (lastOrder is not null)
        {
            lastOrderNumber = lastOrder.OrderNumber;
        }

        Order order = mapper.Map<Order>(request);
        order.OrderNumber = lastOrderNumber + 1;
        order.OrderNumberYear = request.Date.Year;

        await orderRepository.AddAsync(order, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Sipariş bilgisi başarıyla kaydedildi.";

    }

}

