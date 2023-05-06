using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.DocumentType
{
    public class DocumentTypeEntityConfiguration : IEntityTypeConfiguration<DocumentTypeEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentTypeEntity> builder)
        {
            builder.ToTable("DocumentType");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Abbreviation)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
