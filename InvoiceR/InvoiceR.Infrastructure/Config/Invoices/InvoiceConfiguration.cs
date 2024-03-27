using InvoiceR.Domain.Entities.Invoices;
using InvoiceR.Domain.Enums;
using InvoiceR.Infrastructure.Config.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Invoices;

public class InvoiceConfiguration : AuditableEntityConfiguration<Invoice>
{
    public override void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices", "invoices");

        builder.Property(x => x.Number)
           .HasMaxLength(20)
           .IsRequired();

        builder.Property(x => x.Date)
           .HasConversion<DateOnlyConverter>()
            .HasColumnType("date");

        builder.HasOne(s => s.Customer)
           .WithMany(g => g.Invoices)
           .HasForeignKey(s => s.CustomerId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.CustomerId)
           .IsRequired();

        builder.Property(x => x.Description)
           .HasMaxLength(2000);

        builder.Property(x => x.Net)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.Property(x => x.Vat)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.Property(x => x.Gross)
            .HasPrecision(14, 2)
            .IsRequired();

        builder.HasOne(s => s.Currency)
          .WithMany(g => g.Invoices)
          .HasForeignKey(s => s.CurrencyId)
          .OnDelete(DeleteBehavior.NoAction);

        builder.Property(x => x.CurrencyId)
            .IsRequired();

        builder.Property(x => x.Status)
           .HasDefaultValue(InvoiceStatus.Buffer)
           .IsRequired();

        base.Configure(builder);
    }
}