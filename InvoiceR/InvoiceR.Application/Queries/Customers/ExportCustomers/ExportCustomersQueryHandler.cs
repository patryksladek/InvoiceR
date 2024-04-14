using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Queries.Customers.ExportCustomers;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Customers.ExportCustomers;

internal class ExportCustomersQueryHandler : IQueryHandler<ExportCustomersQuery, FileDto>
{
    private readonly ICustomerReadOnlyRepository _customerReadOnlyRepository;
    private readonly IDataExporter _dataExporter;

    public ExportCustomersQueryHandler(ICustomerReadOnlyRepository customerReadOnlyRepository, IDataExporter dataExporter)
    {
        _customerReadOnlyRepository = customerReadOnlyRepository;
        _dataExporter = dataExporter;
    }

    public async Task<FileDto> Handle(ExportCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = _customerReadOnlyRepository.GetByIdsAsync(request.Ids);

        var cusomersDto = await customers.Select(x => new CustomerDto()
        {
            Id = x.Id,
            Name = x.Name,
            NIP = x.NIP,
            Address = $"{x.Address.Street} {x.Address.StreetNumber} {x.Address.Building}",
            City = x.Address.City,
            Country = x.Address.Country.Name,
            Phone = x.Contact.Phone,
            Email = x.Contact.Email
        }).ToListAsync();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(cusomersDto);

        FileDto file = new FileDto
        {
            Name = fileName,
            Content = exportedData
        };

        return file;
    }
}
