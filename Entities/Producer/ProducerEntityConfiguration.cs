using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Producer
{
    public class ProducerEntityConfiguration : IEntityTypeConfiguration<ProducerEntity>
    {
        public void Configure(EntityTypeBuilder<ProducerEntity> builder)
        {
            builder.ToTable("Producer");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(50);
            builder.Property(p => p.Email)
                .HasMaxLength(50);
            builder.Property(p => p.Website)
                .HasMaxLength(50);
        }
    }
}
