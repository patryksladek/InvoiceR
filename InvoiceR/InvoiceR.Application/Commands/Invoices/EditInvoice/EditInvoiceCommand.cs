using InvoiceR.Application.Configuration.Commands;
using MediatR;

namespace InvoiceR.Application.Commands.Invoices.EditInvoice;

public class EditInvoiceCommand : ICommand
{
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public IEnumerable<EditInvoiceItemCommand> InvoiceItems { get; set; }
}

public class EditInvoiceItemCommand
{
    public int? Id { get; set; }
    public int OrdinalNumber { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Net { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public int VatRateId { get; set; }
}