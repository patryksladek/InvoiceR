using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly InvoicerDbContext _dbContext;

    public ProductRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Products
            .Include(x => x.Unit)
            .Include(x => x.VatRate)
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }

    public async Task<bool> IsAlreadyExistWithSameNameAsync(string name, CancellationToken cancellation = default)
    {
        return await _dbContext.Products.AnyAsync(x => x.Name.ToLower() == name.ToLower(), cancellation);
    }

    public async Task<bool> IsAlreadyExistWithSameNameAsync(int id, string name, CancellationToken cancellation = default)
    {
        return await _dbContext.Products.AnyAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower(), cancellation);
    } 

    public void Add(Product product)
    {
        _dbContext.Products.Add(product);
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
    }
}
