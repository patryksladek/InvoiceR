using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(int id, CancellationToken cancellation = default);
    Task<bool> IsAlreadyExistWithSameNameAsync(string name, CancellationToken cancellation = default);
    Task<bool> IsAlreadyExistWithSameNameAsync(int id, string name, CancellationToken cancellation = default);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Customer customer);
}
