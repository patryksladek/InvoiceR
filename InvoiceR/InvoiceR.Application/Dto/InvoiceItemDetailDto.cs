namespace InvoiceR.Application.Dto;

public class InvoiceItemDetailDto
{
    public int Id { get; set; }
    public int OrdinalNumber { get; set; }
    public int ProductId { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Net { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public string Currency { get; set; }
    public int VatRateId { get; set; }
    public string VatRate { get; set; }
}
