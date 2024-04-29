using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Invoices.GetAllInvoice;

public sealed record GetAllInvoiceQuery(int Type) : IRequest<Result<List<Invoice>>>;
