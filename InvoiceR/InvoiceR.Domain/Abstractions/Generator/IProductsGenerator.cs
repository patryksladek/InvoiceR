using InvoiceR.Domain.Entities.Products;

namespace InvoiceR.Domain.Abstractions.Generator;

public interface IProductsGenerator
{
    Task<List<Product>> Generate(int count);
}