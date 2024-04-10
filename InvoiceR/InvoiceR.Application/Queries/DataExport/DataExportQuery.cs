using InvoiceR.Application.Configuration.Queries;
using System.Collections;

namespace InvoiceR.Application.Queries.DataExport;

public record DataExportQuery(IList Data, string FilePath) : IQuery<byte[]>;
