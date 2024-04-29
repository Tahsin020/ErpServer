using AutoMapper;
using ErpServer.Application.Features.Customers.CreateCustomer;
using ErpServer.Application.Features.Customers.UpdateCustomer;
using ErpServer.Application.Features.Depots.CreateDepot;
using ErpServer.Application.Features.Depots.UpdateDepot;
using ErpServer.Application.Features.Invoices.CreateInvoice;
using ErpServer.Application.Features.Invoices.UpdateInvoice;
using ErpServer.Application.Features.Orders.CreateOrder;
using ErpServer.Application.Features.Orders.UpdateOrder;
using ErpServer.Application.Features.Productions.CreateProduction;
using ErpServer.Application.Features.Products.CreateProduct;
using ErpServer.Application.Features.Products.UpdateProduct;
using ErpServer.Application.Features.RecipeDetails.CreateRecipeDetail;
using ErpServer.Application.Features.RecipeDetails.UpdateRecipeDetail;
using ErpServer.Domain.Entities;
using ErpServer.Domain.Enums;

namespace ErpServer.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            CreateMap<CreateDepotCommand, Depot>();
            CreateMap<UpdateDepotCommand, Depot>();

            CreateMap<CreateProductCommand, Product>()
                .ForMember(member => member.Type, options => options.MapFrom(p => ProductTypeEnum.FromValue(p.TypeValue)));

            CreateMap<UpdateProductCommand, Product>()
               .ForMember(member => member.Type, options => options.MapFrom(p => ProductTypeEnum.FromValue(p.TypeValue)));

            CreateMap<CreateRecipeDetailCommand, RecipeDetail>();
            CreateMap<UpdateRecipeDetailCommand, RecipeDetail>();

            CreateMap<CreateOrderCommand, Order>().ForMember(member => member.Details,
                options => options.MapFrom(p => p.Details.Select(s => new OrderDetail
                {
                    Price = s.Price,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity
                }).ToList()));

            CreateMap<UpdateOrderCommand, Order>().ForMember(member => member.Details, options => options.Ignore());

            CreateMap<CreateInvoiceCommand, Invoice>()
                .ForMember(member => member.Type,options => options.MapFrom(p => InvoiceTypeEnum.FromValue(p.TypeValue)))
                .ForMember(member => member.Details,
                options => options.MapFrom(p => p.Details.Select(s => new InvoiceDetail
                {
                    Price = s.Price,
                    ProductId = s.ProductId,
                    Quantity = s.Quantity,
                    DepotId = s.DepotId
                }).ToList()));

            CreateMap<UpdateInvoiceCommand, Invoice>().ForMember(member => member.Details, options => options.Ignore());

            CreateMap<CreateProductionCommand, Production>();

        }
    }
}
