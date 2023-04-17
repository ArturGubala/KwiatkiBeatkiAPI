using System.Data.Entity.ModelConfiguration;

namespace KwiatkiBeatkiAPI.Entities.User
{
    public class UserEntityConfiguration : EntityTypeConfiguration<UserEntity>
    {
        public UserEntityConfiguration()
        {
            ToTable("User");

            Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(50);
            Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            Property(p => p.LastName)
                .HasMaxLength(50);
        }
    }
}
