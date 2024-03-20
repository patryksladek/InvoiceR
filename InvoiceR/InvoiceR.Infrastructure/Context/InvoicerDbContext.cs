using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Products;
using InvoiceR.Infrastructure.Config.Customers;
using InvoiceR.Infrastructure.Config.Definitions;
using InvoiceR.Infrastructure.Config.Products;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Context;

public class InvoicerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<VatRate> VatRates { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    public InvoicerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UnitConfiguration());

        modelBuilder.ApplyConfiguration(new VatRateConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
