using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using MediatR;

namespace InvoiceR.Application.Queries.Customers.GetCustomerById;

internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDetailDto>
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
            StreetNumber = customer.Address.Street,
            Building = customer.Address.Building,
            City = customer.Address.City,
            PostalCode = customer.Address.PostalCode,
            CountryId = customer.Address.CountryId,
            Phone = customer.Contact.Phone,
            Email = customer.Contact.Email,
            Site = customer.Contact.Site
        };

        return customerDto;
    }
}