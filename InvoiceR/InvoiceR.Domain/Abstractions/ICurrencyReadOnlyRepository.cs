using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions;

public interface ICurrencyReadOnlyRepository
{
    public IQueryable<Currency> GetAllAsync();
}