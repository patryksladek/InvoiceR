using FluentValidation;
using FluentValidation.Results;
using InvoiceR.Application.Configuration.Validation.Constatns;

namespace InvoiceR.Application.Commands.Products.AddProduct;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<AddProductCommand> context, CancellationToken cancellation = default)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageConstans.NotEmpty).WithSeverity(Severity.Error)
            .MaximumLength(ProductValidationRuleConstans.MaximumNameLength).WithMessage(ValidationMessageConstans.MaximumLength(ProductValidationRuleConstans.MaximumNameLength)).WithSeverity(Severity.Error);

        return base.ValidateAsync(context, cancellation);
    }
}
