using InvoiceR.Domain.Abstractions;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Repositories;

internal class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoicerDbContext _dbContext;

    public InvoiceRepository(InvoicerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Invoice> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        return await _dbContext.Invoices
            .Include(x => x.Customer)
            .Include(x => x.InvoiceItems)
            .SingleOrDefaultAsync(x => x.Id == id, cancellation);
    }

    /// <summary>
    /// This method returns the available invoice number.
    /// </summary>
    /// <returns>11-character string in format FV/00000/XX</returns>
    public async Task<string> GetNextInvoiceNumberAsync(CancellationToken cancellation = default)
    {
        int currentYear = DateTime.Now.Year;

        string lastFullInvoiceNumber = ((await _dbContext.Invoices.ToListAsync())
            .Where(x => x.Date.Year == currentYear)
            .Last()).Number;

        int lastInvoiceNumber = int.Parse(lastFullInvoiceNumber.Substring(3, 5));

        int newInvoiceNumber = lastInvoiceNumber++;

        string formattedNewInvoiceNumber = newInvoiceNumber.ToString("D5");

        string invoiceNumber = $"FV/{formattedNewInvoiceNumber}/{currentYear.ToString().Substring(2)}";

        return invoiceNumber;
    }

    public void Add(Invoice invoice)
    {
        _dbContext.Invoices.Add(invoice);
    }

    public void Update(Invoice invoice)
    {
        _dbContext.Invoices.Update(invoice);
    }

    public void Delete(Invoice invoice)
    {
        _dbContext.Invoices.Remove(invoice);
    }
}
