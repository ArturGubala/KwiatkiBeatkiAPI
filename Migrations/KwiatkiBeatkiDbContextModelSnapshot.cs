﻿// <auto-generated />
using System;
using KwiatkiBeatkiAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    [DbContext(typeof(KwiatkiBeatkiDbContext))]
    partial class KwiatkiBeatkiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.BulkPack.BulkPackEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BulkPack");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BarCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BulkPackId")
                        .HasColumnType("int");

                    b.Property<int>("ItemTypeId")
                        .HasColumnType("int");

                    b.Property<int>("MeasurementUnitId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProducerId")
                        .HasColumnType("int");

                    b.Property<string>("StockCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BulkPackId");

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ProducerId");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.ItemProperty.ItemPropertyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModificationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PropertyId");

                    b.ToTable("ItemProperty");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.ItemType.ItemTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ItemType");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.MeasurementUnit.MeasurementUnitEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MeasurementUnit");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Producer.ProducerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Producer");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Property.PropertyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Role.RoleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.User.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.BulkPack.BulkPackEntity", "BulkPack")
                        .WithMany()
                        .HasForeignKey("BulkPackId");

                    b.HasOne("KwiatkiBeatkiAPI.Entities.ItemType.ItemTypeEntity", "ItemType")
                        .WithMany()
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.MeasurementUnit.MeasurementUnitEntity", "MeasurementUnit")
                        .WithMany()
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Producer.ProducerEntity", "Producer")
                        .WithMany()
                        .HasForeignKey("ProducerId");

                    b.Navigation("BulkPack");

                    b.Navigation("ItemType");

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.ItemProperty.ItemPropertyEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Property.PropertyEntity", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.User.UserEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.Role.RoleEntity", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
