using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SIM.Domain.Models.Invoice;
using SIM.Domain.Models.Item;

namespace SIM.Persistence.Configurations.Invoice
{
    class InvoiceRowConfiguration : IEntityTypeConfiguration<InvoiceRow>
    {
        public void Configure(EntityTypeBuilder<InvoiceRow> builder)
        {
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.InvoiceId).IsRequired();
            builder.Property(e => e.ItemName).IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Fee).IsRequired();
            builder.Property(e => e.DiscountPercent).IsRequired();
            builder.Property(e => e.FeeAfterDiscount).IsRequired();
            builder.Property(e => e.FeeDiscountPrice).IsRequired();
            builder.Property(e => e.TotalDiscountPrice).IsRequired();
            builder.Property(e => e.PayablePrice).IsRequired();
            builder.Property(e => e.TotalPriceBeforeDiscount).IsRequired();

            builder.HasOne(e => e.Invoice)
                .WithMany(e=>e.InvoiceRows)
                .HasForeignKey(e=>e.InvoiceId);
        }
    }
}
