using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Definitions;
using Mapster;

namespace InvoiceR.Application.Dto;

public class VatRateDto : BaseEntityDto, IMapsterMap
{
    public string Symbol { get; set; }
    public decimal Value { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<VatRate, VatRateDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Symbol, src => src.Symbol)
        .Map(dest => dest.Value, src => src.Value);
    }
}
