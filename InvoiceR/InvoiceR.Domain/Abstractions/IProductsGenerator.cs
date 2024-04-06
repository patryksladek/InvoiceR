using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions;

public interface IProductsGenerator
{
    Task<List<Product>> Generate(int count);
}