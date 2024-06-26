﻿using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class ProductReadOnlyRepository : IProductReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public ProductReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Product> GetAllAsync()
    {
        return _dbContext.Products
            .Include(x => x.Currency)
            .Include(x => x.Unit)
            .Include(x => x.VatRate)
            .AsNoTracking();
    }

    public IQueryable<Product> GetByIdsAsync(int[] ids)
    {
        return _dbContext.Products
            .Include(x => x.Currency)
            .Include(x => x.Unit)
            .Include(x => x.VatRate)
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id));
    }

    public async Task<Product> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Products
             .Include(x => x.Currency)
            .Include(x => x.Unit)
            .Include(x => x.VatRate)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }
}
