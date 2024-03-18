using InvoiceR.Domain.Entities.Definitions;

namespace InvoiceR.Domain.Abstractions;
public interface IVatRateReadOnlyRepository
{
    public IQueryable<VatRate> GetAllAsync();
}

