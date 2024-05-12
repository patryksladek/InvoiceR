namespace InvoiceR.Domain.Abstractions.Servcies;

public interface ICurrencyConverter
{
    decimal Convert(int amount, string fromCurrencySymbol, string toCurrencySymbol, DateTime date);
}
