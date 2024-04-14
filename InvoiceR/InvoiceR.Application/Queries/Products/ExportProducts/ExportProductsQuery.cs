using InvoiceR.Application.Configuration.Queries;
using InvoiceR.Application.Dto;

namespace InvoiceR.Application.Queries.Products.ExportProducts;

public record ExportProductsQuery(int[] Ids, ExportTypeDto ExportType) : IQuery<FileDto>;
