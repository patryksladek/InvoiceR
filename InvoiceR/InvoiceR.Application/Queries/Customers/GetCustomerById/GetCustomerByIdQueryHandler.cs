using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;

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

        var customerDto = new CustomerDetailDto()
        {
            Id = customer.Id,
            Name = customer.Name,
            NIP = customer.NIP,
            Segment = customer.Segment.ToString(),
            IsActive = customer.IsActive,
            Street = customer.Address.Street,
            StreetNumber = customer.Address.StreetNumber,
            Building = customer.Address.Building,
            City = customer.Address.City,
            PostalCode = customer.Address.PostalCode,
            CountryId = customer.Address.Country.Id,
            Phone = customer.Contact.Phone,
            Email = customer.Contact.Email,
            Site = customer.Contact.Site
        };

        return customerDto;
    }
}