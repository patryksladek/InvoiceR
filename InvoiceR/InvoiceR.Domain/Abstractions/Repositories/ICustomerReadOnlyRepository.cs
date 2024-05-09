using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface ICustomerReadOnlyRepository
{
    public IQueryable<Customer> GetAllAsync();
    public IQueryable<Customer> GetByIdsAsync(int[] ids);
    public Task<Customer> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default);
}
