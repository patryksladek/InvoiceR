using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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
        var customers = _customerReadOnlyRepository.GetAllAsync();

        var customersDto = await customers.Select(x => new CustomerDto()
        {
            Id = x.Id,
            Name = x.Name,
            NIP = x.NIP,
            Address = $"{x.Address.Street} {x.Address.StreetNumber} {x.Address.Building}",
            City = x.Address.City,
            Country = x.Address.Country.Name,
            Phone = x.Contact.Phone,
            Email = x.Contact.Email
        })
        .ToListAsync();

        return customersDto;
    }
}
