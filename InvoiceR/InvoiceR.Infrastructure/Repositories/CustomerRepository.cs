using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace InvoiceR.Infrastructure.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly InvoicerDbContext _dbContext;

    public CustomerRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Customers
            .Include(x => x.Address).ThenInclude(x => x.Country)
            .Include(x => x.Contact)
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }

    public async Task<bool> IsAlreadyExistWithSameNameAsync(string name, CancellationToken cancellation = default)
    {
        return await _dbContext.Customers.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellation);
    }

    public async Task<bool> IsAlreadyExistWithSameNameAsync(int id, string name, CancellationToken cancellation = default)
    {
        return await _dbContext.Customers.AnyAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower(), cancellation);
    }

    public void Add(Customer customer)
    {
        _dbContext.Customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        _dbContext.Customers.Update(customer);
    }

    public void Delete(Customer customer)
    {
        _dbContext.Customers.Remove(customer);
    }
}
