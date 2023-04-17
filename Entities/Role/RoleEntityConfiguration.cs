using System.Data.Entity.ModelConfiguration;

namespace KwiatkiBeatkiAPI.Entities.Role
{
    public class RoleEntityConfiguration : EntityTypeConfiguration<RoleEntity>
    {
        public RoleEntityConfiguration()
        {
            ToTable("Role");

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
