namespace InvoiceR.Application.Dto;

public class InvoiceDetailDto
{
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public IEnumerable<InvoiceItemDetailDto> InvoiceItems { get; set; }
}
