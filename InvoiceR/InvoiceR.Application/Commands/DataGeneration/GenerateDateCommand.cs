using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.DataGeneration;

public record GenerateDateCommand(int customersCount, int productsCount, int invoicesCount) : ICommand;
