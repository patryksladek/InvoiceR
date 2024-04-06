namespace InvoiceR.Domain.Abstractions;

public interface IDataGenerator
{
    Task GenerateData(int customersCount, int productsCount, int invoicesCount);
    bool IsNoData();
}