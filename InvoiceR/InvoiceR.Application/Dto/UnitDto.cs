using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Products;
using Mapster;

namespace InvoiceR.Application.Dto;

public class UnitDto : BaseEntityDto, IMapsterMap
{
    public string Code { get; set; }
    public string Description { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Unit, UnitDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Code, src => src.Code)
        .Map(dest => dest.Description, src => src.Description);
    }
}
