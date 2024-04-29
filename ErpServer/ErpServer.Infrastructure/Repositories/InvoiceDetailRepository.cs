using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using ErpServer.Infrastructure.Context;
using GenericRepository;

namespace ErpServer.Infrastructure.Repositories;

internal sealed class InvoiceDetailRepository : Repository<InvoiceDetail, ApplicationDbContext>, IInvoiceDetailRepository
{
    public InvoiceDetailRepository(ApplicationDbContext context) : base(context)
    {
    }
}
