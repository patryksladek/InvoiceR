using Bogus;
using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Context;

namespace InvoiceR.Infrastructure.DataGeneration.Generators;

public class ProductsGenerator : IProductsGenerator
{
    private readonly InvoicerDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;   

    public ProductsGenerator(InvoicerDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Product>> Generate(int count)
    {
        var products = GenerateProducts(count);
        
        _dbContext.Products.AddRange(products);
        await _unitOfWork.SaveChangesAsync();

        return products;
    }

    private List<Product> GenerateProducts(int count)
    {
        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Type, f => f.PickRandom<ProductType>())
            .RuleFor(p => p.Barcode, f => f.Commerce.Ean13())
            .RuleFor(p => p.BarcodeType, f => f.PickRandom<ProductBarcodeType>())
            .RuleFor(p => p.NetPrice, f => f.Random.Decimal(10, 1000))
            .RuleFor(i => i.CurrencyId, f => f.Random.Int(1, 3))
            .RuleFor(i => i.UnitId, f => f.Random.Int(1, 3))
            .RuleFor(i => i.VatRateId, f => f.Random.Int(1, 9));

        return productFaker.Generate(count);
    }
}
