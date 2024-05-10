using Bogus;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Context;

namespace InvoiceR.Infrastructure.DataGeneration.Generators;

public class CustomersGenerator : ICustomersGenerator
{
    private readonly InvoicerDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public CustomersGenerator(InvoicerDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Customer>> Generate(int count)
    {
        var customers = GenerateCustomers(count);

        _dbContext.Customers.AddRange(customers);
        await _unitOfWork.SaveChangesAsync();

        return customers;
    }

    private List<Customer> GenerateCustomers(int count)
    {
        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.NIP, f => f.Random.Replace("##########"))
            .RuleFor(c => c.Segment, f => f.PickRandom<CustomerSegment>())
            .RuleFor(c => c.Address, f => GenerateAddress(f))
            .RuleFor(c => c.Contact, f => GenerateContact(f))
            .RuleFor(c => c.IsActive, f => f.Random.Bool());

        return customerFaker.Generate(count);
    }
    private Address GenerateAddress(Faker faker)
    {
        return new Address
        {
            Street = faker.Address.StreetName(),
            StreetNumber = faker.Address.BuildingNumber(),
            City = faker.Address.City(),
            PostalCode = faker.Address.ZipCode(),
            CountryId = faker.Random.Number(1, 242)
        };
    }
    private Contact GenerateContact(Faker faker)
    {
        return new Contact
        {
            Phone = faker.Phone.PhoneNumber(),
            Email = faker.Internet.Email(),
            Site = faker.Internet.Url()
        };
    }
}
