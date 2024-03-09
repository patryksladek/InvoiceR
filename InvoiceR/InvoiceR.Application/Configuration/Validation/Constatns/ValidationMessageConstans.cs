namespace InvoiceR.Application.Configuration.Validation.Constatns;

public static class ValidationMessageConstans
{
    public static string NotEmpty => "Field is required.";
    public static string MaximumLength(int maximumLength) => $"The field can have a maximum length of {maximumLength} characters.";
    public static string MinimumLength(int minimumLength) => $"The field can have a minimum length of {minimumLength} characters.";
    public static string IncorrectEmailAddress => "Incorrect email address format.";
    public static string MinimumValue(int minimumValue) => $"The value cannot be less than {minimumValue}.";
}
