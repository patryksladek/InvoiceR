using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Enums;
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
        var invoices = _invoiceReadOnlyRepository.GetByIdsAsync(request.Ids);

        var invoicesDto = await invoices.Select(x => new InvoiceDto()
        {
            Id = x.Id,
            IsApproved = x.Status == InvoiceStatus.Confirmed ? true : false,
            Number = x.Number,
            Date = x.Date,
            Customer = $"{x.Customer.Name}",
            Net = x.Net,
            Vat = x.Vat,
            Gross = x.Gross,
            Currency = x.Currency.Symbol,
            InvoiceItems = x.InvoiceItems.Select(x => new InvoiceItemDto()
            {
                OrdinalNumber = x.OrdinalNumber,
                Product = x.Product.Name,
                Quantity = x.Quantity,
                Price = x.Price,
                Net = x.Net,
                Gross = x.Gross,
                Currency = x.Currency.Symbol
            })
        }).ToListAsync();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(invoicesDto);

        return new FileDto(fileName, exportedData);
    }
}
