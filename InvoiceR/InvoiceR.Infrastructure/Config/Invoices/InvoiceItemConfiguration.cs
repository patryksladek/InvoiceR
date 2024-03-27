using InvoiceR.Domain.Entities.Invoices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Invoices;

public class InvoiceItemConfiguration : AuditableEntityConfiguration<InvoiceItem>
{
    public override void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItems", "invoices");

        builder.HasOne(s => s.Invoice)
           .WithMany(g => g.InvoiceItems)
           .HasForeignKey(s => s.InvoiceId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.OrdinalNumber)
           .IsRequired();

        builder.HasOne(s => s.Product)
           .WithMany(g => g.InvoiceItems)
           .HasForeignKey(s => s.ProductId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.Quantity)
          .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.Property(x => x.Net)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.Property(x => x.Gross)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.HasOne(s => s.Currency)
           .WithMany(g => g.InvoiceItems)
           .HasForeignKey(s => s.CurrencyId);

        builder.HasOne(s => s.VatRate)
          .WithMany(g => g.InvoiceItems)
          .HasForeignKey(s => s.VatRateId);

        base.Configure(builder);
    }
}