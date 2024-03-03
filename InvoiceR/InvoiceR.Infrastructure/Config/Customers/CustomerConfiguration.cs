using InvoiceR.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Customers;

public class CustomerConfiguration : AuditableEntityConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", "customers");

        builder.Property(s => s.Name)
           .HasMaxLength(240)
           .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.Property(s => s.NIP)
           .HasMaxLength(17);

        builder.Property(s => s.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(x => x.Address)
            .WithOne(x => x.Customer)
            .HasForeignKey<Address>(x => x.CustomerId);

        builder.HasOne(x => x.Contact)
           .WithOne(x => x.Customer)
           .HasForeignKey<Contact>(x => x.CustomerId);

        base.Configure(builder);
    }
}

