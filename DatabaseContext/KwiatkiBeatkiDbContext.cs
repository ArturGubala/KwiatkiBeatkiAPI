using KwiatkiBeatkiAPI.Models.Settings;
using Microsoft.EntityFrameworkCore;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Role;

namespace KwiatkiBeatkiAPI.DatabaseContext
{
    public class KwiatkiBeatkiDbContext : DbContext
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Role { get; set; }

        public KwiatkiBeatkiDbContext(DbContextOptions<KwiatkiBeatkiDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>();
            modelBuilder.Entity<RoleEntity>();
        }
    }
}
