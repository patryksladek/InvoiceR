using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly InvoicerDbContext _dbContext;

    public UnitOfWork(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = _dbContext
            .ChangeTracker
            .Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = entry.Entity.UpdatedAt = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.Now;
            }
        }
    }
}
