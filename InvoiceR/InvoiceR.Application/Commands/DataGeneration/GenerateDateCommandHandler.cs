using InvoiceR.Application.Commands.Customers.AddCustomer;
using InvoiceR.Application.Configuration.Commands;
using InvoiceR.Application.Dto;
using InvoiceR.Domain.Abstractions;
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

        await _dataGenerator.GenerateData(request.customersCount, request.productsCount, request.invoicesCount);
    }
}
