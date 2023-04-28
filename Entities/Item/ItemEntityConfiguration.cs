using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Item
{
    public class ItemEntityConfiguration : IEntityTypeConfiguration<ItemEntity>
    {
        public void Configure(EntityTypeBuilder<ItemEntity> builder)
        {
            builder.ToTable("Item");

            builder.Property(p => p.ItemTypeId)
                .IsRequired();
            builder.Property(p => p.MeasurementUnitId)
                .IsRequired();
            builder.Property(p => p.StockCode)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Name)
                .HasMaxLength(100);
            builder.Property(p => p.Alias)
                .HasMaxLength(50);
            builder.Property(p => p.BarCode)
                .HasMaxLength(50);
        }
    }
}
