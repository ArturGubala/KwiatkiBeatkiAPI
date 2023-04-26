using KwiatkiBeatkiAPI.Models.Settings;
using Microsoft.EntityFrameworkCore;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Role;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Entities.Producer;

namespace KwiatkiBeatkiAPI.DatabaseContext
{
    public class KwiatkiBeatkiDbContext : DbContext
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<RoleEntity> Role { get; set; }
        public DbSet<MeasurementUnitEntity> MeasurementUnit { get; set; }
        public DbSet<ItemTypeEntity> ItemType { get; set; }
        public DbSet<BulkPackEntity> BulkPack { get; set; }
        public DbSet<ProducerEntity> Producer { get; set; }

        public KwiatkiBeatkiDbContext(DbContextOptions<KwiatkiBeatkiDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>();
            modelBuilder.Entity<RoleEntity>();
            modelBuilder.Entity<MeasurementUnitEntity>();
            modelBuilder.Entity<ItemTypeEntity>();
            modelBuilder.Entity<BulkPackEntity>();
            modelBuilder.Entity<ProducerEntity>();
        }
    }
}
