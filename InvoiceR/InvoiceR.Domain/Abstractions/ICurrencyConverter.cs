namespace InvoiceR.Domain.Abstractions;

public interface ICurrencyConverter
{
    decimal Convert(int amount, string fromCurrencySymbol, string toCurrencySymbol, DateTime date);
}
