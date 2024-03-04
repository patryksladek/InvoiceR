using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public CustomerReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Customer> GetAllAsync()
    {
        return _dbContext.Customers
            .Include(x => x.Address).ThenInclude(x => x.Country)
            .Include(x => x.Contact)
            .AsNoTracking();
    }

    public async Task<Customer> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Customers
            .Include(x => x.Address).ThenInclude(x => x.Country)
            .Include(x => x.Contact)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }
}