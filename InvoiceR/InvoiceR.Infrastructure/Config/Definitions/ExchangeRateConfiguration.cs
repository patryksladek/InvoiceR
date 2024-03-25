using InvoiceR.Domain.Entities.Definitions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Definitions;

public class ExchangeRateConfiguration : BaseEntityConfiguration<ExchangeRate>
{
    public override void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.ToTable("ExchangeRates", "definitions");

        builder.HasOne(s => s.Currency)
            .WithMany(g => g.ExchangeRates)
            .HasForeignKey(s => s.CurrencyId);

        builder.Property(x => x.Date)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        builder.Property(x => x.Rate)
            .HasPrecision(16, 4)
            .IsRequired();

        builder.Property(x => x.TableNumber)
          .HasMaxLength(240)
          .IsRequired();

        base.Configure(builder);
    }
}
