using FluentValidation;
using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Customers.EditCusotmer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoiceR.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        services.AddScoped<IValidator<AddCustomerCommand>, AddCustomerCommandValidator>();
        services.AddScoped<IValidator<EditCustomerCommand>, EditCustomerCommandValidator>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        return app;
    }
}