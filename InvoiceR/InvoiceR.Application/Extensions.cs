using FluentValidation;
using InvoiceR.Application.Behaviors;
using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Commands.Customers.EditCusotmer;
using InvoiceR.Application.Mapping;
using InvoiceR.Application.Middlewares;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InvoiceR.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddMapster();
        MapsterConfig.RegisterMappings();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
        
        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}