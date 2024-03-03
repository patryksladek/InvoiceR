using InvoiceR.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceR.Infrastructure.Config.Customers;

public class ContactConfiguration : AuditableEntityConfiguration<Contact>
{
    public override void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.ToTable("Contacts", "customers");

        builder.Property(x => x.Phone)
            .HasMaxLength(30);

        builder.Property(x => x.Email)
            .HasMaxLength(100);

        builder.Property(x => x.Site)
            .HasMaxLength(100);

        base.Configure(builder);
    }
}
