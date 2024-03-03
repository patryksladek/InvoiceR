using InvoiceR.Domain.Entities.Customers;
using InvoiceR.Infrastructure.Config.Customers;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Context;

public class InvoicerDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Country> Countries { get; set; }

    public InvoicerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CountryConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
