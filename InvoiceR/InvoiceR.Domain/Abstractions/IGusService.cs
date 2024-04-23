namespace InvoiceR.Domain.Abstractions;

public interface IGusService
{
    Task<string> GetSearchResultByNipAsync(string nip);
}
