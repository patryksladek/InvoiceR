using FluentValidation;
using FluentValidation.Results;
using InvoiceR.Application.Configuration.Validation.Constatns;

namespace InvoiceR.Application.Commands.Products.EditProduct;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<EditProductCommand> context, CancellationToken cancellation = default)
    {
        RuleFor(x => x.Name)
           .NotEmpty().WithMessage(ValidationMessageConstans.NotEmpty).WithSeverity(Severity.Error)
           .MaximumLength(ProductValidationRuleConstans.MaximumNameLength).WithMessage(ValidationMessageConstans.MaximumLength(ProductValidationRuleConstans.MaximumNameLength)).WithSeverity(Severity.Error);

        return base.ValidateAsync(context, cancellation);
    }
}
