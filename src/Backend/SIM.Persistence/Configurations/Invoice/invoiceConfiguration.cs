using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIM.Domain.Models.Invoice;
using SIM.Domain.Models.Item;

namespace SIM.Persistence.Configurations.Invoice
{
    class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Models.Invoice.Invoice>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Invoice.Invoice> builder)
        {
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Number).IsRequired();
            builder.Property(e => e.CreatedOn).IsRequired();
            builder.Property(e => e.BuyerName).IsRequired();
            builder.Property(e => e.TotalPrice).IsRequired();
            builder.Property(e => e.TotalDiscount).IsRequired();
            builder.Property(e => e.PayablePrice).IsRequired();
            builder.Property(e => e.Description).IsRequired();

            builder.HasMany(e => e.InvoiceRows)
                .WithOne(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
