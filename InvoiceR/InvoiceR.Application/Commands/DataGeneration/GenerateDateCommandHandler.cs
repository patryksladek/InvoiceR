using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Domain.Exceptions;

namespace InvoiceR.Application.Commands.DataGeneration;

internal class GenerateDateCommandHandler : ICommandHandler<GenerateDateCommand>
{
    private readonly IDataGenerator _dataGenerator;

    public GenerateDateCommandHandler(IDataGenerator dataGenerator)
    {
        _dataGenerator = dataGenerator;
    }

    public async Task Handle(GenerateDateCommand request, CancellationToken cancellationToken)
    {
        if (!_dataGenerator.IsNoData())
            throw new NotEmptyDatabaseException();

        await _dataGenerator.GenerateData(request.CustomersCount, request.ProductsCount, request.InvoicesCount);
    }
}
