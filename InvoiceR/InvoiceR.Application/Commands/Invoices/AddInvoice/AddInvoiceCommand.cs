using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Commands.Invoices.AddInvoice;

public class AddInvoiceCommand : ICommand<InvoiceDetailDto>
{
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public IEnumerable<AddInvoiceItemCommand> InvoiceItems { get; set; }
}

public class AddInvoiceItemCommand
{
    public int OrdinalNumber { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Net { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public int VatRateId { get; set; }
}
