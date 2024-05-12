using InvoiceR.Domain.Abstractions.Servcies;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceR.Infrastructure.Gus;

public static class Extensions
{
    public static IServiceCollection AddGusService(this IServiceCollection services)
    {
        services.AddScoped<IGusService, GusService>();

        return services;
    }
}