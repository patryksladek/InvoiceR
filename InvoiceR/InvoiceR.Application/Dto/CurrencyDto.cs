using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Definitions;
using Mapster;

namespace InvoiceR.Application.Dto;

public class CurrencyDto : BaseEntityDto, IMapsterMap
{
    public string Symbol { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Currency, CurrencyDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Symbol, src => src.Symbol);
    }
}
