using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions;

public interface ICustomerReadOnlyRepository
{
    public IQueryable<Customer> GetAllAsync();
    public Task<Customer> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default);
}
