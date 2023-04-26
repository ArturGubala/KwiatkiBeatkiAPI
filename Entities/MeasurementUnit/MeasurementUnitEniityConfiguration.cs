using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.MeasurementUnit
{
    public class MeasurementUnitEniityConfiguration : IEntityTypeConfiguration<MeasurementUnitEntity>
    {
        public void Configure(EntityTypeBuilder<MeasurementUnitEntity> builder)
        {
            builder
                .ToTable("MeasurementUnit");

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            builder.Property(p => p.Abbreviation)
                     .IsRequired()
                     .HasMaxLength(5);
        }
    }
}
