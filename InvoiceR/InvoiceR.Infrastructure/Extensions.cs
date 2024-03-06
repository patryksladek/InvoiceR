using InvoiceR.Domain.Abstractions;
using InvoiceR.Infrastructure.Context;
using InvoiceR.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceR.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICustomerReadOnlyRepository, CustomerReadOnlyRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICountryReadOnlyRepository, CountryReadOnlyRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddDbContext<InvoicerDbContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("InvoiceR")));

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
