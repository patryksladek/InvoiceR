﻿using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Abstractions.Servcies;
using InvoiceR.Infrastructure.Context;
using InvoiceR.Infrastructure.Converters;
using InvoiceR.Infrastructure.DataExport;
using InvoiceR.Infrastructure.DataGeneration;
using InvoiceR.Infrastructure.Gus;
using InvoiceR.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;

namespace InvoiceR.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerReadOnlyRepository, CustomerReadOnlyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICountryReadOnlyRepository, CountryReadOnlyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductReadOnlyRepository, ProductReadOnlyRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUnitReadOnlyRepository, UnitReadOnlyRepository>();
        services.AddScoped<IVatRateReadOnlyRepository, VatRateReadOnlyRepository>();
        services.AddScoped<ICurrencyReadOnlyRepository, CurrencyReadOnlyRepository>();
        services.AddScoped<IInvoiceReadOnlyRepository, InvoiceReadOnlyRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<ICurrencyConverter, CurrencyConverter>();
        services.AddScoped<IExchangeRateReadOnlyRepository, ExchangeRateReadOnlyRepository>();
        services.AddDataGenerator();
        services.AddDataExporter();
        services.AddGusService();

        services.AddDbContext<InvoicerDbContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("InvoiceR")));

        return services;
    }
    public static ConfigureHostBuilder UseInfrastructure(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseNLog();

        return hostBuilder;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetService<InvoicerDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }
}
