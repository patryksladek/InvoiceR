using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;
using Mapster;

namespace InvoiceR.Application.Dto;

public class InvoiceDto : BaseEntityDto, IMapsterMap
{
    public bool IsApproved { get; set; }
    public string Number { get; set; }
    public DateOnly Date { get; set; }
    public string Customer { get; set; }
    public decimal Net { get; set; }
    public decimal Vat { get; set; }
    public decimal Gross { get; set; }
    public string Currency { get; set; }
    public virtual IEnumerable<InvoiceItemDto> InvoiceItems { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Invoice, InvoiceDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.IsApproved, src => src.Status == InvoiceStatus.Confirmed ? true : false)
        .Map(dest => dest.Number, src => src.Number)
        .Map(dest => dest.Date, src => src.Date)
        .Map(dest => dest.Customer, src => src.Customer.Name)
        .Map(dest => dest.Net, src => src.Net)
        .Map(dest => dest.Vat, src => src.Vat)
        .Map(dest => dest.Gross, src => src.Gross)
        .Map(dest => dest.Currency, src => src.Currency.Symbol)
        .Map(dest => dest.InvoiceItems, src => src.InvoiceItems);
    }
}
