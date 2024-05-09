using InvoiceR.Domain.Entities.Customers;

namespace InvoiceR.Domain.Abstractions.Repositories;

public interface ICountryReadOnlyRepository
{
    public IQueryable<Country> GetAllAsync();
}