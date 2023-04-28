using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.User
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {

            builder.ToTable("User");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(p => p.FirstName)
                .HasMaxLength(50);
            builder.Property(p => p.LastName)
                .HasMaxLength(50);
        }
    }
}
