using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions;

public interface ICountryReadOnlyRepository
{
    public IQueryable<Country> GetAllAsync();
}