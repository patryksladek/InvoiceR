using InvoiceR.Domain.Abstractions;
using InvoiceR.Infrastructure.Context;

namespace InvoiceR.Infrastructure.DataGeneration;

public class DataGenerator : IDataGenerator
{
    private readonly InvoicerDbContext _dbContext;

    private readonly ICustomersGenerator _customersGenerator;
    private readonly IProductsGenerator _productsGenerator;
    private readonly IInvoicesGenerator _invoicesGenerator;

    public DataGenerator(InvoicerDbContext dbContext, ICustomersGenerator customersGenerator, IProductsGenerator productsGenerator, IInvoicesGenerator invoicesGenerator)
    {
        _dbContext = dbContext;

        _customersGenerator = customersGenerator;
        _productsGenerator = productsGenerator;
        _invoicesGenerator = invoicesGenerator;
    }

    public bool IsNoData()
        => _dbContext.Customers.Count() == 0 && _dbContext.Products.Count() == 0 && _dbContext.Invoices.Count() == 0;

    public async Task GenerateData(int customersCount, int productsCount, int invoicesCount)
    {
        var customers = await _customersGenerator.Generate(customersCount);
        var products = await _productsGenerator.Generate(productsCount);
        await _invoicesGenerator.Generate(invoicesCount, customers, products);
    }
}