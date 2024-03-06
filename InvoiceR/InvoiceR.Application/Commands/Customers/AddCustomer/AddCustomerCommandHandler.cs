using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Enums;
using InvoiceR.Domain.Exceptions;
using MediatR;

namespace InvoiceR.Application.Commands.Customers.AddCustomer;

internal class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, CustomerDetailDto>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDetailDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isAlreadyExistWithSameName = await _customerRepository.IsAlreadyExistWithSameNameAsync(request.Name, cancellationToken);
        if (isAlreadyExistWithSameName)
            throw new CustomerWithSameNameAlreadyExistsException(request.Name);

        var newCustomer = new Customer()
        {
            Name = request.Name,
            NIP = request.NIP,
            Segment = request.Segment.HasValue ? (CustomerSegment)request.Segment.Value : null,
            IsActive = request.IsActive,
            Address = new Address()
            {
                Street = request.Street,
                StreetNumber = request.StreetNumber,
                Building = request.Building,
                PostalCode = request.PostalCode,
                City = request.City,
                CountryId = request.CountryId,
            },
            Contact = new Contact()
            {
                Phone = request.Phone,
                Email = request.Email,
                Site = request.Site
            }
        };

        _customerRepository.Add(newCustomer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var customerDto = new CustomerDetailDto()
        {
            Id = newCustomer.Id,
            Name = newCustomer.Name,
            NIP = newCustomer.NIP,
            Segment = newCustomer.Segment.ToString(),
            IsActive = newCustomer.IsActive,
            Street = newCustomer.Address.Street,
            StreetNumber = newCustomer.Address.Street,
            Building = newCustomer.Address.Building,
            City = newCustomer.Address.City,
            PostalCode = newCustomer.Address.PostalCode,
            CountryId = newCustomer.Address.CountryId,
            Phone = newCustomer.Contact.Phone,
            Email = newCustomer.Contact.Email,
            Site = newCustomer.Contact.Site
        };

        return customerDto;
    }
}
