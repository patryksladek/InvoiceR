using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Customers.ExportCustomers;

internal class ExportInvoicesQueryHandler : IQueryHandler<ExportInvoicesQuery, FileDto>
{
    private readonly IInvoiceReadOnlyRepository _invoiceReadOnlyRepository;
    private readonly IDataExporter _dataExporter;

    public ExportInvoicesQueryHandler(IInvoiceReadOnlyRepository invoiceReadOnlyRepository, IDataExporter dataExporter)
    {
        _invoiceReadOnlyRepository = invoiceReadOnlyRepository;
        _dataExporter = dataExporter;
    }

    public async Task<FileDto> Handle(ExportInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceReadOnlyRepository.GetByIdsAsync(request.Ids).ToListAsync();

        var invoicesDto = invoices.Adapt<IReadOnlyCollection<InvoiceDto>>();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Invoices);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(invoicesDto);

        return new FileDto(fileName, exportedData);
    }
}
