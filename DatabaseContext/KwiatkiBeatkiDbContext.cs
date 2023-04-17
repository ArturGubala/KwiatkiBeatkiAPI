using KwiatkiBeatkiAPI.Models.Settings;
using Microsoft.EntityFrameworkCore;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Role;

namespace KwiatkiBeatkiAPI.DatabaseContext
{
    public class KwiatkiBeatkiDbContext : DbContext
    {
        private readonly DatabaseInfo _databaseInfo;
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Role { get; set; }

        public KwiatkiBeatkiDbContext(DatabaseInfo databaseInfo)
        {
            _databaseInfo = databaseInfo;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>();
            modelBuilder.Entity<RoleEntity>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_databaseInfo.ConnectionString);
        }
    }
}
