using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;
using Mapster;

namespace InvoiceR.Application.Dto;

public class InvoiceDetailDto : BaseEntityDto, IMapsterMap
{
    public int Id { get; set; }
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public int CustomerId { get; set; }
    public string Description { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public int CurrencyId { get; set; }
    public IEnumerable<InvoiceItemDetailDto> InvoiceItems { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Invoice, InvoiceDetailDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.IsApproved, src => src.Status == InvoiceStatus.Confirmed ? true : false)
        .Map(dest => dest.Number, src => src.Number)
        .Map(dest => dest.Date, src => src.Date)
        .Map(dest => dest.CustomerId, src => src.CustomerId)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.Net, src => src.Net)
        .Map(dest => dest.Vat, src => src.Vat)
        .Map(dest => dest.Gross, src => src.Gross)
        .Map(dest => dest.CurrencyId, src => src.CurrencyId)
        .Map(dest => dest.InvoiceItems, src => src.InvoiceItems);
    }
}
