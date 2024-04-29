using ErpServer.Domain.Dtos;
using ErpServer.Domain.Entities;
using ErpServer.Domain.Enums;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace ErpServer.Application.Features.Orders.RequirementsPlanningByOrderId;

internal sealed class RequirementsPlanningByOrderIdCommandHandler(IOrderRepository orderRepository, IStockMovementRepository stockMovementRepository, IRecipeRepository recipeRepository, IUnitOfWork unitOfWork) : IRequestHandler<RequirementsPlanningByOrderIdCommand, Result<RequirementsPlanningByOrderIdCommandRespose>>
{
    public async Task<Result<RequirementsPlanningByOrderIdCommandRespose>> Handle(RequirementsPlanningByOrderIdCommand request, CancellationToken cancellationToken)
    {
        Order? order = await orderRepository
            .Where(p => p.Id == request.OrderId)
            .Include(p => p.Details!)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(cancellationToken);

        if (order is null)
        {
            return Result<RequirementsPlanningByOrderIdCommandRespose>.Failure("Sipariş bulunamadı");
        }

        List<ProductDto> uretilmesiGerekenUrunListesi = new();
        List<ProductDto> requirementsPlanningProducts = new();

        if (order.Details is not null)
        {
            foreach (var item in order.Details)
            {
                var product = item.Product;
                List<StockMovement> movements = await stockMovementRepository
                    .Where(p => p.ProductId == product!.Id)
                    .ToListAsync(cancellationToken);

                decimal stock = movements.Sum(p => p.NumberOfEntries) - movements.Sum(p => p.NumberOfOutputs);

                if (stock < item.Quantity)
                {
                    ProductDto uretilmesiGerekenUrun = new()
                    {
                        Id = item.ProductId,
                        Quantity = item.Quantity - stock,
                        Name = product!.Name
                    };

                    uretilmesiGerekenUrunListesi.Add(uretilmesiGerekenUrun);

                }
            }

            foreach (var item in uretilmesiGerekenUrunListesi)
            {
                Recipe? recipe = await recipeRepository
                    .Where(p => p.ProductId == item.Id)
                    .Include(p => p.Details!)
                    .ThenInclude(p => p.Product)
                    .FirstOrDefaultAsync(cancellationToken);

                if (recipe is not null && recipe.Details is not null)
                {
                    foreach (var detail in recipe.Details)
                    {
                        List<StockMovement> urunMovements = await stockMovementRepository
                            .Where(p => p.ProductId == detail!.ProductId)
                            .ToListAsync(cancellationToken);

                        decimal stock = urunMovements.Sum(p => p.NumberOfEntries) - urunMovements.Sum(p => p.NumberOfOutputs);

                        if (stock < detail.Quantity)
                        {
                            ProductDto ihtiyacOlanUrun = new()
                            {
                                Id = detail.ProductId,
                                Quantity = detail.Quantity - stock,
                                Name = detail.Product!.Name
                            };
                            requirementsPlanningProducts.Add(ihtiyacOlanUrun);

                        }
                    }
                }
            }
        }

        requirementsPlanningProducts = requirementsPlanningProducts
            .GroupBy(p => p.Id)
            .Select(g => new ProductDto
            {
                Id = g.Key,
                Name = g.First().Name,
                Quantity = g.Sum(item => item.Quantity)
            }).ToList();

        order.Status = OrderStatusEnum.RequirementsPlanWorked;
        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new RequirementsPlanningByOrderIdCommandRespose(DateOnly.FromDateTime(DateTime.Now), order.Number + "Nolu Siparişin İhtiyaç Planlaması", requirementsPlanningProducts);
    }
}
