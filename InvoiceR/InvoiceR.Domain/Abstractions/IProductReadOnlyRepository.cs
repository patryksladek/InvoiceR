using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions;

public interface IProductReadOnlyRepository
{
    public IQueryable<Product> GetAllAsync();
    public IQueryable<Product> GetByIdsAsync(int[] ids);
    public Task<Product> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default);
}
