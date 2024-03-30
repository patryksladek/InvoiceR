using FluentValidation;
using FluentValidation.Results;

namespace InvoiceR.Application.Commands.Invoices.AddInvoice;

public class AddInvoiceCommandValidator : AbstractValidator<AddInvoiceCommand>
{
    public AddInvoiceCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<AddInvoiceCommand> context, CancellationToken cancellation = default)
    {
        return base.ValidateAsync(context, cancellation);
    }
}
