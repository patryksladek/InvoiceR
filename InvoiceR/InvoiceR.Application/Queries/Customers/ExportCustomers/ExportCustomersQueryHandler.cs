using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;
using Mapster;
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
        var customers = await _customerReadOnlyRepository.GetByIdsAsync(request.Ids).ToListAsync();

        var customersDto = customers.Adapt<IEnumerable<CustomerDto>>();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(customersDto);

        return new FileDto(fileName, exportedData);
    }
}
