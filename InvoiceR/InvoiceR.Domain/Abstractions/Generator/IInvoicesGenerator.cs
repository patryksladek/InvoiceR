using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions.Generator;

public interface IInvoicesGenerator
{
    Task<List<Invoice>> Generate(int count, List<Customer> customers, List<Product> products);
}