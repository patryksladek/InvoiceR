using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class ExchangeRateReadOnlyRepository : IExchangeRateReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public ExchangeRateReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<ExchangeRate> GetAllAsync()
    {
        return _dbContext.ExchangeRates.AsNoTracking();
    }
}