using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.DataGeneration;

public record GenerateDateCommand(int CustomersCount, int ProductsCount, int InvoicesCount) : ICommand;
