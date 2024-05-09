using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface ICurrencyReadOnlyRepository
{
    public IQueryable<Currency> GetAllAsync();
}