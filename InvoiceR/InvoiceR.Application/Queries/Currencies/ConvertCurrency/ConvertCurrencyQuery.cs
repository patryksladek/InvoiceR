using InvoiceR.Application.Configuration.Queries;

namespace InvoiceR.Application.Queries.Currencies.ConvertCurrency;

public record ConvertCurrencyQuery(string FromCurrencySymbol, string ToCurrencySymbol, DateTime Date, int Amount = 1) : IQuery<decimal>;