using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;

namespace InvoiceR.Domain.Entities.Customers;

public class Customer : AuditableEntity
{
    public string Name { get; set; }
    public string NIP { get; set; }
    public CustomerSegment? Segment { get; set; }
    public Address Address { get; set; }
    public Contact Contact { get; set; }
    public bool IsActive { get; set; }
    public ICollection<Invoice> Invoices { get; set; }
}