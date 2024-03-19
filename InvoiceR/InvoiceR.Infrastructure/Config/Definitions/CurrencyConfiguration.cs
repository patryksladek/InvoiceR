using InvoiceR.Domain.Entities;
using InvoiceR.Domain.Entities.Definitions;
using InvoiceR.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Definitions;

public class CurrencyConfiguration : BaseEntityConfiguration<Currency>
{
    public override void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("Currencies", "definitions");

        builder.Property(x => x.Symbol)
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(x => x.Name)
           .HasPrecision(240)
           .IsRequired();

        builder.Property(x => x.IsDefault)
           .HasDefaultValue(false)
           .IsRequired();

        base.Configure(builder);
    }
}