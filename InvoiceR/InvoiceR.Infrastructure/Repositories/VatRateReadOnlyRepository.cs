using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class VatRateReadOnlyRepository : IVatRateReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public VatRateReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<VatRate> GetAllAsync()
    {
        return _dbContext.VatRates.AsNoTracking();
    }
}