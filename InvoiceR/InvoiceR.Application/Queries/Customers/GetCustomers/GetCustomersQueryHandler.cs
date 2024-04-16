using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Customers.GetCustomers;

internal class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, IReadOnlyCollection<CustomerDto>>
{
    private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;

    public GetCustomersQueryHandler(ICustomerReadOnlyRepository customerReadOnlyRepository)
    {
        _customerReadOnlyRepository = customerReadOnlyRepository;
    }

    public async Task<IReadOnlyCollection<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _customerReadOnlyRepository.GetAllAsync().ToListAsync();

        var customersDto = customers.Adapt<IReadOnlyCollection<CustomerDto>>();

        return customersDto;
    }
}
