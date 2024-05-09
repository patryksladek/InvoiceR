using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface IExchangeRateReadOnlyRepository
{
    public IQueryable<ExchangeRate> GetAllAsync();
}

