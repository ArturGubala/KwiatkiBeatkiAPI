using Microsoft.EntityFrameworkCore;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Role;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Entities.Property;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.Warehouse;
using KwiatkiBeatkiAPI.Entities.DocumentType;
using KwiatkiBeatkiAPI.Entities.TradePartner;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Entities.Document;

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
        public DbSet<PropertyEntity> Property { get; set; }
        public DbSet<ItemPropertyEntity> ItemProperty { get; set; }
        public DbSet<ItemEntity> Item { get; set; }
        public DbSet<WarehouseEntity> Warehouse { get; set; }
        public DbSet<DocumentTypeEntity> DocumentType { get; set; }
        public DbSet<TradePartnerEntity> TradePartner { get; set; }
        public DbSet<LineEntity> Line { get; set; }
        public DbSet<DocumentEntity> Document { get; set; }

        public KwiatkiBeatkiDbContext(DbContextOptions<KwiatkiBeatkiDbContext> options) : base (options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(u => u.Documents)
                .WithOne(d => d.User)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RoleEntity>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<MeasurementUnitEntity>()
                .HasMany(m => m.Items)
                .WithOne(i => i.MeasurementUnit)
                .HasForeignKey(i => i.MeasurementUnitId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemTypeEntity>()
                .HasMany(it => it.Items)
                .WithOne(i => i.ItemType)
                .HasForeignKey(i => i.ItemTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BulkPackEntity>()
                .HasMany(b => b.Items)
                .WithOne(i => i.BulkPack)
                .HasForeignKey(i => i.BulkPackId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProducerEntity>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Producer)
                .HasForeignKey(i => i.ProducerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<PropertyEntity>()
                .HasMany(p => p.ItemProperties)
                .WithOne(ip => ip.Property)
                .HasForeignKey(ip => ip.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemPropertyEntity>();
            modelBuilder.Entity<ItemEntity>()
                .HasMany(i => i.Lines)
                .WithOne(l => l.Item)
                .HasForeignKey(i => i.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WarehouseEntity>()
                .HasMany(w => w.DocsWithWarehousesFrom)
                .WithOne(d => d.WarehouseFrom)
                .HasForeignKey(d => d.WarehouseFromId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<WarehouseEntity>()
                .HasMany(w => w.DocsWithWarehousesTo)
                .WithOne(d => d.WarehouseTo)
                .HasForeignKey(d => d.WarehouseToId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<DocumentTypeEntity>()
                .HasMany(dt => dt.Documents)
                .WithOne(d => d.DocumentType)
                .HasForeignKey(d => d.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TradePartnerEntity>()
                .HasMany(t => t.Documents)
                .WithOne(d => d.TradePartner)
                .HasForeignKey(d => d.TradePartnerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<LineEntity>();
            modelBuilder.Entity<DocumentEntity>();
        }
    }
}
