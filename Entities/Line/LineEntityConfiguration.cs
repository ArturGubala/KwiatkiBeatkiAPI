using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Line
{
    public class LineEntityConfiguration : IEntityTypeConfiguration<LineEntity>
    {
        public void Configure(EntityTypeBuilder<LineEntity> builder)
        {
            builder.ToTable("Line");

            builder.Property(p => p.DocumentId)
                .IsRequired();
            builder.Property(p => p.ItemId)
                .IsRequired();
            builder.Property(p => p.Quantity)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
            builder.Property(p => p.Remarks)
                .HasMaxLength(300);
        }
    }
}
