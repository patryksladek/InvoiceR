using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.Products.EditProduct;

public class EditProductCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProductType { get; set; }
    public string Barcode { get; set; }
    public int ProductBarcodeType { get; set; }
    public int CurrencyId { get; set; }
    public int UnitId { get; set; }
    public int VatRateId { get; set; }
    public decimal Price { get; set; }
}
