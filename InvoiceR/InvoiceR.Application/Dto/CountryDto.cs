using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Customers;
using Mapster;

namespace InvoiceR.Application.Dto;

public class CountryDto : BaseEntityDto, IMapsterMap
{
    public string Symbol { get; set; }
    public string Name { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Country, CountryDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Symbol, src => src.Symbol)
        .Map(dest => dest.Name, src => src.Name);
    }
}
