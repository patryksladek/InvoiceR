using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using Mapster;

namespace InvoiceR.Application.Queries.Customers.GetCustomerById;

internal class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, CustomerDetailDto>
{
    private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;

    public GetCustomerByIdQueryHandler(ICustomerReadOnlyRepository customerReadOnlyRepository)
    {
        _customerReadOnlyRepository = customerReadOnlyRepository;
    }

    public async Task<CustomerDetailDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadOnlyRepository.GetByIdWithDetailAsync(request.Id);

        var customerDto = customer.Adapt<CustomerDetailDto>();

        return customerDto;
    }
}