using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Document
{
    public class DocumentEntityConfiguration : IEntityTypeConfiguration<DocumentEntity>
    {
        public void Configure(EntityTypeBuilder<DocumentEntity> builder)
        {
            builder.ToTable("Document");

            builder.Property(p => p.DocumentTypeId)
                .IsRequired();
            builder.Property(p => p.UserId)
                .IsRequired();
            builder.Property(p => p.FullDocumentNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.DocumentNumber)
                .IsRequired();
            builder.Property(p => p.Remarks)
                .HasMaxLength(300);
        }
    }
}
