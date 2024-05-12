namespace InvoiceR.Domain.Abstractions.Servcies;

public interface IGusService
{
    Task<string> GetSearchResultByNipAsync(string nip);
}
