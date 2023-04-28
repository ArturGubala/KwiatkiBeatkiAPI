using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KwiatkiBeatkiAPI.Entities.Role
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);
        }
    }
}
