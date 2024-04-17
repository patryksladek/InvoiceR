using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Products;
using Mapster;

namespace InvoiceR.Application.Dto;

public class ProductDetailDto : BaseEntityDto, IMapsterMap
{
    public string Name { get; set; }
    public string ProductType { get; set; }
    public string Barcode { get; set; }
    public string ProductBarcodeType { get; set; }
    public int CurrencyId { get; set; }
    public int UnitId { get; set; }
    public int VatRateId { get; set; }
    public decimal Price { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Product, ProductDetailDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.ProductType, src => src.Type.ToString())
        .Map(dest => dest.Barcode, src => src.Barcode)
        .Map(dest => dest.ProductBarcodeType, src => src.BarcodeType.ToString())
        .Map(dest => dest.Price, src => src.NetPrice)
        .Map(dest => dest.CurrencyId, src => src.CurrencyId)
        .Map(dest => dest.UnitId, src => src.UnitId)
        .Map(dest => dest.VatRateId, src => src.VatRateId);
    }
}
