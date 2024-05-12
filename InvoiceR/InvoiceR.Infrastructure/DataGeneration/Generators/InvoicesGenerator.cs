using Bogus;
using InvoiceR.Domain.Abstractions.Generator;
using InvoiceR.Domain.Abstractions.Repositories;
using InvoiceR.Domain.Abstractions.Servcies;
using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Context;

namespace InvoiceR.Infrastructure.DataGeneration.Generators;

public class InvoicesGenerator : IInvoicesGenerator
{
    private readonly InvoicerDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrencyConverter _currencyConverter;

    public InvoicesGenerator(InvoicerDbContext dbContext, IUnitOfWork unitOfWork, ICurrencyConverter currencyConverter)
    {
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
        _currencyConverter = currencyConverter;
    }

    public async Task<List<Invoice>> Generate(int count, List<Customer> customers, List<Product> products)
    {
        var invoices = GenerateInvoices(count, customers, products);
        
        _dbContext.Invoices.AddRange(invoices);
        await _unitOfWork.SaveChangesAsync();

        return invoices;
    }

    private List<Invoice> GenerateInvoices(int count, List<Customer> customers, List<Product> products)
    {
        var currencies = _dbContext.Currencies;

        var invoiceFaker = new Faker<Invoice>()
            .RuleFor(i => i.Number, f => "FV/" + f.Random.Number(0, 99999).ToString("D5") + "/23")
            .RuleFor(i => i.Date, f => DateOnly.FromDateTime(f.Date.Recent(200)))
            .RuleFor(i => i.CustomerId, f => f.PickRandom(customers).Id)
            .RuleFor(i => i.CurrencyId, currencies.Single(x => x.IsDefault).Id)
            .RuleFor(i => i.Status, f => f.PickRandom<InvoiceStatus>());

        var invoices = invoiceFaker.Generate(count);

        foreach (var invoice in invoices)
        {
            invoice.InvoiceItems = GenerateInvoiceItems(new Random().Next(1, 10), invoice, products);

            CalculateSummary(invoice);
        }

        return invoices;
    }

    private List<InvoiceItem> GenerateInvoiceItems(int count, Invoice invoice, List<Product> products)
    {
        var vatRates = _dbContext.VatRates;

        var itemFaker = new Faker<InvoiceItem>()
            .RuleFor(ii => ii.InvoiceId, invoice.Id)
            .RuleFor(ii => ii.ProductId, f => f.PickRandom(products).Id)
            .RuleFor(ii => ii.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(iI => iI.CurrencyId, f => f.PickRandom(products).CurrencyId)
            .RuleFor(ii => ii.VatRateId, f => f.PickRandom(products).VatRateId)
            .RuleFor(ii => ii.Price, f => f.Random.Decimal(10, 1000))
            .RuleFor(ii => ii.Net, (f, ii) => ii.Price * ii.Quantity)
            .RuleFor(ii => ii.Gross, (f, ii) => ii.Net + (ii.Net * vatRates.Single(x => x.Id == ii.VatRateId).Value));

        var invoiceItems = new List<InvoiceItem>();
        int ordinalNumber = 1;

        for (int i = 0; i < count; i++)
        {
            var invoiceItem = itemFaker.Generate();
            invoiceItem.OrdinalNumber = ordinalNumber++; // Zwiększ ordinalNumber
            invoiceItems.Add(invoiceItem);
        }

        return invoiceItems;
    }

    private void CalculateSummary(Invoice invoice)
    {
        var currencyList = _dbContext.Currencies;

        decimal summaryNet = 0;
        decimal summaryVat = 0;
        decimal summaryGross = 0;

        foreach (var invoiceItem in invoice.InvoiceItems)
        {
            string fromCurrencySymbol = currencyList.Single(x => x.Id == invoiceItem.CurrencyId).Symbol;
            string toCurrencySymbol = currencyList.Single(x => x.IsDefault).Symbol;
            DateTime date = invoice.Date.ToDateTime(TimeOnly.Parse("00:00"));

            decimal converter = _currencyConverter.Convert(1, fromCurrencySymbol, toCurrencySymbol, date);

            decimal net = invoiceItem.Net * converter;
            summaryNet += net;
            decimal gross = invoiceItem.Gross * converter;
            summaryGross += gross;

            decimal vat = gross - net;
            summaryVat += vat;
        }

        invoice.Net = summaryNet;
        invoice.Gross = summaryVat;
        invoice.Vat = summaryGross;
    }

}
