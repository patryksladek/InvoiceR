using InvoiceR.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Customers;

public class AddressConfiguration : AuditableEntityConfiguration<Address>
{
    public override void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("Addresses", "customers");

        builder.Property(x => x.Street)
            .HasMaxLength(100);

        builder.Property(x => x.StreetNumber)
           .HasMaxLength(50);

        builder.Property(x => x.Building)
           .HasMaxLength(50);

        builder.Property(x => x.PostalCode)
         .HasMaxLength(16);

        builder.Property(x => x.City)
          .HasMaxLength(100);

        builder.Property(x => x.CountryId)
         .IsRequired();

        builder.HasOne(x => x.Country)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.CountryId);

        base.Configure(builder);
    }
}
