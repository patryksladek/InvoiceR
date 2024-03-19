using InvoiceR.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Products;

public class ProductConfiguration : AuditableEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "products");

        builder.Property(x => x.Name)
           .HasMaxLength(240)
           .IsRequired();

        builder.HasIndex(x => x.Name)
           .IsUnique();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Barcode)
            .HasMaxLength(130);

        builder.HasOne(s => s.Currency)
            .WithMany(g => g.Products)
            .HasForeignKey(s => s.CurrencyId);

        builder.HasOne(s => s.VatRate)
           .WithMany(g => g.Products)
           .HasForeignKey(s => s.VatRateId);

        builder.Property(x => x.NetPrice)
            .HasPrecision(14, 2)
            .IsRequired();

        base.Configure(builder);
    }
}
