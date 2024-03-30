using InvoiceR.Application.Configuration.Commands;
using MediatR;

namespace InvoiceR.Application.Commands.Invoices.RemoveInvoice;

public record RemoveInvoiceCommand(int Id) : ICommand;
