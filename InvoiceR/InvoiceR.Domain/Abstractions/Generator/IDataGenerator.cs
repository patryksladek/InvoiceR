namespace InvoiceR.Domain.Abstractions.Generator;

public interface IDataGenerator
{
    Task GenerateData(int customersCount, int productsCount, int invoicesCount);
    bool IsNoData();
}