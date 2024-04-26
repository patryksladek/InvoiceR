using InvoiceR.Application.Dto;
using System.Xml.Linq;

namespace InvoiceR.Application.Parsers;

public class GusXmlParser
{
    public CustomerDetailDto ParseCustomerXml(string xmlString)
    {
        var xmlDoc = XDocument.Parse(xmlString);
        var customerElement = xmlDoc.Element("root")?.Element("dane");
        if (customerElement == null)
        {
            throw new ArgumentException("Invalid XML format: 'root' or 'dane' element not found.");
        }

        var customerDto = new CustomerDetailDto
        {
            Name = customerElement.Element("Nazwa")?.Value,
            NIP = customerElement.Element("Nip")?.Value,
            Street = customerElement.Element("Ulica")?.Value,
            StreetNumber = customerElement.Element("NrNieruchomosci")?.Value,
            Building = customerElement.Element("NrLokalu")?.Value,
            PostalCode = customerElement.Element("KodPocztowy")?.Value,
            City = customerElement.Element("Miejscowosc")?.Value
        };

        return customerDto;
    }
}