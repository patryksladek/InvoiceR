using InvoiceR.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace InvoiceR.Infrastructure.Config.Customers;

public class CountryConfiguration : BaseEntityConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries", "customers");

        builder.Property(x => x.Symbol)
            .HasMaxLength(2)
            .IsRequired();

        builder.Property(x => x.Name)
           .HasMaxLength(50)
           .IsRequired();

        base.Configure(builder);
    }
}
