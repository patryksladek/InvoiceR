using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions;

public interface IExchangeRateReadOnlyRepository
{
    public IQueryable<ExchangeRate> GetAllAsync();
}

