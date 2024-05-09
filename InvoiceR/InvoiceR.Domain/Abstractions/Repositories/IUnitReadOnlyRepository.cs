using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface IUnitReadOnlyRepository
{
    public IQueryable<Unit> GetAllAsync();
}
