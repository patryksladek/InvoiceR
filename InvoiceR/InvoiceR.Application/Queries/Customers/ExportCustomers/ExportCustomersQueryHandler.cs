using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;

namespace InvoiceR.Application.Queries.DataExport;

internal class ExportCustomersQueryHandler : IQueryHandler<ExportCustomersQuery, FileDto>
{
    private readonly IDataExporter _dataExporter;

    public ExportCustomersQueryHandler(IDataExporter dataExporter)
    {
        _dataExporter = dataExporter;
    }

    public async Task<FileDto> Handle(ExportCustomersQuery request, CancellationToken cancellationToken)
    {

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        byte[] exportedData = _dataExporter.ExportData(request.Data);

        FileDto file = new FileDto
        {
            Name = fileName,
            Content = exportedData
        };

        return file;
    }
}
