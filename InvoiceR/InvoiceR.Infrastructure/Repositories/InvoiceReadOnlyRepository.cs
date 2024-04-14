using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class InvoiceReadOnlyRepository : IInvoiceReadOnlyRepository
{
    private readonly InvoicerDbContext _dbContext;

    public InvoiceReadOnlyRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Invoice> GetAllAsync()
    {
        return _dbContext.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Currency)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Product)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Currency)
            .AsNoTracking();
    }

    public IQueryable<Invoice> GetByIdsAsync(int[] ids)
    {
        return _dbContext.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Currency)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Product)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Currency)
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id));
    }

    public async Task<Invoice> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Invoices
            .Include(x => x.Customer)
            .Include(x => x.Currency)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Product)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.Currency)
            .Include(x => x.InvoiceItems).ThenInclude(x => x.VatRate)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }

    public async Task<InvoiceItem> GetInvoiceItemByIdWithDetailAsync(int itemId, CancellationToken cancellation = default)
    {
        return await _dbContext.InvoiceItems
            .Include(x => x.Product)
            .Include(x => x.Currency)
            .Include(x => x.VatRate)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == itemId, cancellation);
    }
}
