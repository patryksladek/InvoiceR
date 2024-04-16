using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Customers;
using Mapster;

namespace InvoiceR.Application.Dto;

public class CustomerDetailDto : BaseEntityDto, IMapsterMap
{
    public string Name { get; set; }
    public string NIP { get; set; }
    public string Segment { get; set; }
    public bool IsActive { get; set; }
    public string Street { get; set; }
    public string StreetNumber { get; set; }
    public string Building { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public int CountryId { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Site { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Customer, CustomerDetailDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.NIP, src => src.NIP)
        .Map(dest => dest.Segment, src => src.Segment.ToString())
        .Map(dest => dest.IsActive, src => src.IsActive)
        .Map(dest => dest.Street, src => src.Address.Street)
        .Map(dest => dest.StreetNumber, src => src.Address.StreetNumber)
        .Map(dest => dest.Building, src => src.Address.Building)
        .Map(dest => dest.City, src => src.Address.City)
        .Map(dest => dest.PostalCode, src => src.Address.PostalCode)
        .Map(dest => dest.CountryId, src => src.Address.CountryId)
        .Map(dest => dest.Phone, src => src.Contact.Phone)
        .Map(dest => dest.Email, src => src.Contact.Email)
        .Map(dest => dest.Site, src => src.Contact.Site);
    }
}
