using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class CountryReadOnlyRepository : ICountryReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public CountryReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Country> GetAllAsync()
    {
        return _dbContext.Countries.AsNoTracking();
    }
}
