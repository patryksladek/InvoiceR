using InvoiceR.Application.Mapping;
using InvoiceR.Domain.Entities.Customers;
using Mapster;
using System;

namespace InvoiceR.Application.Dto;

public class CustomerDto : BaseEntityDto, IMapsterMap
{
    public string Name { get; set; }
    public string NIP { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public void ConfigureMapping()
    {
        TypeAdapterConfig<Customer, CustomerDto>.NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.NIP, src => src.NIP)
        .Map(dest => dest.Address, src => $"{src.Address.Street} {src.Address.StreetNumber} {src.Address.Building}")
        .Map(dest => dest.City, src => src.Address.City)
        .Map(dest => dest.Country, src => src.Address.Country.Name)
        .Map(dest => dest.Phone, src => src.Contact.Phone)
        .Map(dest => dest.Email, src => src.Contact.Email);
    }
}
