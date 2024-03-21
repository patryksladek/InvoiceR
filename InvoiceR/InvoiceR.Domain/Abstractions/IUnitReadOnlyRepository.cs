using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions;

public interface IUnitReadOnlyRepository
{
    public IQueryable<Unit> GetAllAsync();
}
