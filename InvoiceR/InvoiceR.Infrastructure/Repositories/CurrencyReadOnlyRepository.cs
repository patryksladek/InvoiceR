using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class CurrencyReadOnlyRepository : ICurrencyReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public CurrencyReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Currency> GetAllAsync()
    {
        return _dbContext.Currencies.AsNoTracking();
    }
}