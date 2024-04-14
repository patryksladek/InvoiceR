using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Application.Queries.Products.ExportProducts;

internal class ExportProductsQueryHandler : IQueryHandler<ExportProductsQuery, FileDto>
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IDataExporter _dataExporter;

    public ExportProductsQueryHandler(IProductReadOnlyRepository productReadOnlyRepository, IDataExporter dataExporter)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
        _dataExporter = dataExporter;
    }

    public async Task<FileDto> Handle(ExportProductsQuery request, CancellationToken cancellationToken)
    {
        var products = _productReadOnlyRepository.GetByIdsAsync(request.Ids);

        var productsDto = await products.Select(x => new ProductDto()
        {
            Id = x.Id,
            Name = x.Name,
            Barcode = x.Barcode,
            Price = x.NetPrice,
            Currency = x.Currency.Symbol
        }).ToListAsync();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(productsDto);

        FileDto file = new FileDto
        {
            Name = fileName,
            Content = exportedData
        };

        return file;
    }
}
