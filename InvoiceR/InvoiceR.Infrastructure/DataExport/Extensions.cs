using Microsoft.Extensions.DependencyInjection;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Infrastructure.DataExport.Factories;

namespace InvoiceR.Infrastructure.DataExport;

public static class Extensions
{
    public static IServiceCollection AddDataExporter(this IServiceCollection services)
    {
        services.AddScoped<IExportStrategyFactory, ExportStrategyFactory>();
        services.AddScoped<IDataExporter, DataExporter>();

        return services;
    }
}
