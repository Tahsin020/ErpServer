using ErpServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Customers.GetAllCustomer;

public sealed record GetAllCustomerQuery() : IRequest<Result<List<Customer>>>;
