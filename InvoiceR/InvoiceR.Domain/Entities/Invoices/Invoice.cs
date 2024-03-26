using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Enums;

namespace InvoiceR.Domain.Entities.Invoices;

public class Invoice : AuditableEntity
{
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public string Description { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public InvoiceStatus Status { get; set; }
    public ICollection<InvoiceItem> InvoiceItems { get; set; }
}