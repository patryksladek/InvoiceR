using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Products;
using Mapster;

namespace InvoiceR.Application.Dto;

public class ProductDto : BaseEntityDto, IMapsterMap
{
    public string Name { get; set; }
    public string Barcode { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Barcode, src => src.Barcode)
        .Map(dest => dest.Price, src => src.NetPrice)
        .Map(dest => dest.Currency, src => src.Currency.Symbol);
    }
}
