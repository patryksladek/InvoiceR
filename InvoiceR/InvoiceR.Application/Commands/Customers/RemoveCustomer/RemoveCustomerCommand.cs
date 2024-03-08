using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.Customers.RemoveCustomer;

public record RemoveCustomerCommand(int Id) : ICommand;
