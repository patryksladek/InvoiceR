using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

public class UnitReadOnlyRepository : IUnitReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public UnitReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Unit> GetAllAsync()
    {
        return _dbContext.Units.AsNoTracking();
    }
}
