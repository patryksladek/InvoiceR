using FluentValidation;
using FluentValidation.Results;
using InvoiceR.Application.Configuration.Validation.Constatns;

namespace InvoiceR.Application.Commands.Customers.AddCustomer;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;
    }

    public override Task<ValidationResult> ValidateAsync(ValidationContext<AddCustomerCommand> context, CancellationToken cancellation = default)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(ValidationMessageConstans.NotEmpty).WithSeverity(Severity.Error)
            .MaximumLength(CustomerValidationRuleConstans.MaximumNameLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumNameLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage(ValidationMessageConstans.NotEmpty).WithSeverity(Severity.Error)
            .MaximumLength(CustomerValidationRuleConstans.MaximumCodeLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumCodeLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.NIP)
            .MaximumLength(CustomerValidationRuleConstans.MaximumNipLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumNipLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.Street)
            .MaximumLength(CustomerValidationRuleConstans.MaximumStreetLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumStreetLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.StreetNumber)
            .MaximumLength(CustomerValidationRuleConstans.MaximumStreetNumberLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumStreetNumberLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.Building)
            .MaximumLength(CustomerValidationRuleConstans.MaximumBuildingLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumBuildingLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.City)
            .MaximumLength(CustomerValidationRuleConstans.MaximumCityLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumCityLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.PostalCode)
           .MaximumLength(CustomerValidationRuleConstans.MaximumPostalCodeLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumPostalCodeLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.Phone)
            .MaximumLength(CustomerValidationRuleConstans.MaximumPhoneLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumPhoneLength)).WithSeverity(Severity.Error);

        RuleFor(x => x.Email)
            .MaximumLength(CustomerValidationRuleConstans.MaximumEmailLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumEmailLength)).WithSeverity(Severity.Error)
            .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email)).WithMessage(ValidationMessageConstans.IncorrectEmailAddress).WithSeverity(Severity.Warning);

        RuleFor(x => x.Site)
           .MaximumLength(CustomerValidationRuleConstans.MaximumSiteLength).WithMessage(ValidationMessageConstans.MaximumLength(CustomerValidationRuleConstans.MaximumSiteLength)).WithSeverity(Severity.Error);

        return base.ValidateAsync(context, cancellation);
    }
}

