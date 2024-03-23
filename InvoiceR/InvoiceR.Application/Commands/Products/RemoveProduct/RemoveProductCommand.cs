using InvoiceR.Application.Configuration.Commands;

namespace InvoiceR.Application.Commands.Products.RemoveProduct;

public record RemoveProductCommand(int Id) : ICommand;
