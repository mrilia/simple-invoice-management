using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SIM.Persistence.Configurations.Item
{
    class ItemConfiguration : IEntityTypeConfiguration<Domain.Models.Item.Item>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Item.Item> builder)
        {
            builder.Property(e => e.Id).IsRequired().UseIdentityColumn();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Fee).IsRequired();

            builder.HasMany(e => e.ItemsInInvoices)
                .WithOne(e => e.Item)
                .HasForeignKey(e => e.ItemId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
