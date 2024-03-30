using FluentValidation;
using FluentValidation.Results;

namespace InvoiceR.Application.Commands.Invoices.EditInvoice;

public class EditInvoiceCommandValidator : AbstractValidator<EditInvoiceCommand>
{
    public EditInvoiceCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<EditInvoiceCommand> context, CancellationToken cancellation = default)
    {
        return base.ValidateAsync(context, cancellation);
    }
}
