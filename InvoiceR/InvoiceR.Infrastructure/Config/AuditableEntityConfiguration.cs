using InvoiceR.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InvoiceR.Infrastructure.Config;

public abstract class AuditableEntityConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedAt)
            .HasColumnType("datetime2(0)")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnType("datetime2(0)")
            .IsRequired();
    }
}