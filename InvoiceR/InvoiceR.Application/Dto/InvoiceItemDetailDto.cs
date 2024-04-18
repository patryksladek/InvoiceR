using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Invoices;
using Mapster;

namespace InvoiceR.Application.Dto;

public class InvoiceItemDetailDto : BaseEntityDto, IMapsterMap
{
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

    public void ConfigureMapping()
    {
        TypeAdapterConfig<InvoiceItem, InvoiceItemDetailDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.OrdinalNumber, src => src.OrdinalNumber)
        .Map(dest => dest.Product, src => src.Product.Name)
        .Map(dest => dest.Quantity, src => src.Quantity)
        .Map(dest => dest.Price, src => src.Price)
        .Map(dest => dest.Net, src => src.Net)
        .Map(dest => dest.Gross, src => src.Gross)
        .Map(dest => dest.CurrencyId, src => src.CurrencyId)
        .Map(dest => dest.Currency, src => src.Currency.Symbol)
        .Map(dest => dest.VatRateId, src => src.VatRateId)
        .Map(dest => dest.VatRate, src => src.VatRate.Symbol);
    }
}
