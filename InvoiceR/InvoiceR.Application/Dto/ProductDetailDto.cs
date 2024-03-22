namespace InvoiceR.Application.Dto;

public class ProductDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string ProductType { get; set; }
    public string Barcode { get; set; }
    public string ProductBarcodeType { get; set; }
    public int CurrencyId { get; set; }
    public int UnitId { get; set; }
    public int VatRateId { get; set; }
    public decimal Price { get; set; }
}
