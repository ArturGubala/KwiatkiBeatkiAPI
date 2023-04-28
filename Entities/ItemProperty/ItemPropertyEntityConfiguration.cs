using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.ItemProperty
{
    public class ItemPropertyEntityConfiguration : IEntityTypeConfiguration<ItemPropertyEntity>
    {
        public void Configure(EntityTypeBuilder<ItemPropertyEntity> builder)
        {
            builder.ToTable("Property");

            builder.Property(p => p.ItemId)
                .IsRequired();
            builder.Property(p => p.PropertyId)
                .IsRequired();
            builder.Property(p => p.Value)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
