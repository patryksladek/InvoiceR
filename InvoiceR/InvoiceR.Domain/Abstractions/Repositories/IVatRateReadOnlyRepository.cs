using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions.Repositories;
public interface IVatRateReadOnlyRepository
{
    public IQueryable<VatRate> GetAllAsync();
}

