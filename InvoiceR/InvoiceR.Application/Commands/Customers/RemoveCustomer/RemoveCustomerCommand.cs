using MediatR;

namespace InvoiceR.Application.Commands.Customers.RemoveCustomer;

public record RemoveCustomerCommand(int Id) : IRequest;
