namespace InvoiceR.Domain.Entities.Definitions;

public class ExchangeRate : BaseEntity
{
    public int CurrencyId { get; set; }
    public Currency Currency { get; set; }
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
    public string TableNumber { get; set; }
}
