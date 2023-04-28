using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.BulkPack
{
    public class BulkPackEntityConfiguration : IEntityTypeConfiguration<BulkPackEntity>
    {
        public void Configure(EntityTypeBuilder<BulkPackEntity> builder)
        {
            builder.ToTable("BulkPack");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Abbreviation)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
