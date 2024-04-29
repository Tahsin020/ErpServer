using AutoMapper;
using ErpServer.Domain.Entities;
using ErpServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace ErpServer.Application.Features.Customers.CreateCustomer;

internal sealed class CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCustomerCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isTaxNumberExists = await customerRepository.AnyAsync(p => p.TaxNumber == request.TaxNumber, cancellationToken);

        if (isTaxNumberExists)
        {
            return Result<string>.Failure("Vergi numarası daha önce kaydedilmiş");
        }

        Customer customer = mapper.Map<Customer>(request);

        await customerRepository.AddAsync(customer);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return "Müşteri kaydı başarıyla eklendi.";
    }
}
