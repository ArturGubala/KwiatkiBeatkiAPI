using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.ItemType
{
    public class BulkPackEntityConfiguration : IEntityTypeConfiguration<ItemTypeEntity>
    {
        public void Configure(EntityTypeBuilder<ItemTypeEntity> builder)
        {
            builder
                .ToTable("ItemType");

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
        }
    }
}
