using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;
using InvoiceR.Application.Utilities;
using InvoiceR.Domain.Abstractions.DataExporter;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using Mapster;
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
        var products = await _productReadOnlyRepository.GetByIdsAsync(request.Ids).ToListAsync();

        var productsDto = products.Adapt<IReadOnlyCollection<ProductDto>>();

        ExportType exportType = EnumMapper.Map<ExportType>(request.ExportType);
        string fileName = FileNameGenerator.GenerateFileName(exportType, ExportObject.Customers);

        _dataExporter.SetExportStrategy(exportType);
        byte[] exportedData = _dataExporter.ExportData(productsDto);

        return new FileDto(fileName, exportedData);
    }
}
