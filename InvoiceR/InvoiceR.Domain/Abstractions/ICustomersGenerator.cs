using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions;

public interface ICustomersGenerator
{
    Task<List<Customer>> Generate(int count);
}