namespace InvoiceR.Application.Dto;

public class InvoiceItemDto
{
    public int OrdinalNumber { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Net { get; set; }
    public decimal Gross { get; set; }
    public string Currency { get; set; }
}
