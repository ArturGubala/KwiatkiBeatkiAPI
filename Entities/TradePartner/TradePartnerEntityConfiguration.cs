using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.TradePartner
{
    public class TradePartnerEntityConfiguration : IEntityTypeConfiguration<TradePartnerEntity>
    {
        public void Configure(EntityTypeBuilder<TradePartnerEntity> builder)
        {
            builder.ToTable("TradePartner");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.Email)
                .HasMaxLength(50);
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(50);
            builder.Property(p => p.Website)
                .HasMaxLength(50);
            builder.Property(p => p.Street)
                .HasMaxLength(50);
            builder.Property(p => p.StreetNumber)
                .HasMaxLength(50);
            builder.Property(p => p.City)
                .HasMaxLength(50);
            builder.Property(p => p.PostalCode)
                .HasMaxLength(50);
            builder.Property(p => p.Nip)
                .HasMaxLength(50);
        }
    }
}
