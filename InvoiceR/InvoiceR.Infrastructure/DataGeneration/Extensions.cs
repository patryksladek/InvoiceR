using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Infrastructure.DataGeneration.Generators;
using InvoiceR.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceR.Infrastructure.DataGeneration;

public static class Extensions
{
    public static IServiceCollection AddDataGenerator(this IServiceCollection services)
    {
        services.AddScoped<ICustomersGenerator, CustomersGenerator>();
        services.AddScoped<IProductsGenerator, ProductsGenerator>();
        services.AddScoped<IInvoicesGenerator, InvoicesGenerator>();
        services.AddScoped<IDataGenerator, DataGenerator>();

        return services;
    }
}
