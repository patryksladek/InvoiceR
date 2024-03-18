using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id, CancellationToken cancellation = default);
    Task<string> GetNextProductCodeAsync(CancellationToken cancellation = default);
    Task<bool> IsAlreadyExistWithSameNameAsync(string name, CancellationToken cancellation = default);
    Task<bool> IsAlreadyExistWithSameNameAsync(int id, string name, CancellationToken cancellation = default);
    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);
}
