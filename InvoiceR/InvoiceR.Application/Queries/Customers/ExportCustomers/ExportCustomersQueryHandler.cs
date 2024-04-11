using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions.DataExporter;

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
        string fileName = "exported_data.csv";

        byte[] exportedData = _dataExporter.ExportData(request.Data);

        FileDto file = new FileDto
        {
            Name = fileName,
            Content = exportedData
        };

        return file;
    }
}
