using InvoiceR.Domain.Entities.Invoices;

namespace InvoiceR.Domain.Abstractions;

public interface IInvoiceReadOnlyRepository
{
    public IQueryable<Invoice> GetAllAsync();
    public Task<Invoice> GetByIdWithDetailAsync(int id, CancellationToken cancellation = default);
    public Task<InvoiceItem> GetInvoiceItemByIdWithDetailAsync(int itemId, CancellationToken cancellation = default);
}
