using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Property
{
    public class PropertyEntityConfiguration : IEntityTypeConfiguration<PropertyEntity>
    {
        public void Configure(EntityTypeBuilder<PropertyEntity> builder)
        {
            builder.ToTable("Property");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}
