﻿// <auto-generated />
using System;
using KwiatkiBeatkiAPI.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KwiatkiBeatkiAPI.Migrations
{
    [DbContext(typeof(KwiatkiBeatkiDbContext))]
    [Migration("20230512164944_AddIndexesForNameColumnInTradePartnerProducerProperty")]
    partial class AddIndexesForNameColumnInTradePartnerProducerProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Document.DocumentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("DocumentNumber")
                        .HasColumnType("int");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FullDocumentNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TradePartnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseFromId")
                        .HasColumnType("int");

                    b.Property<int?>("WarehouseToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("TradePartnerId");

                    b.HasIndex("UserId");

                    b.HasIndex("WarehouseFromId");

                    b.HasIndex("WarehouseToId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.DocumentType.DocumentTypeEntity", b =>
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

                    b.ToTable("DocumentType");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(450)");

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Alias")
                        .IsUnique()
                        .HasFilter("[Alias] IS NOT NULL");

                    b.HasIndex("BulkPackId");

                    b.HasIndex("ItemTypeId");

                    b.HasIndex("MeasurementUnitId");

                    b.HasIndex("ProducerId");

                    b.HasIndex("StockCode")
                        .IsUnique();

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

                    b.HasIndex("ItemId", "PropertyId")
                        .IsUnique();

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

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Line.LineEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("DocumentId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId");

                    b.HasIndex("ItemId");

                    b.ToTable("Line");
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
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

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
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.TradePartner.TradePartnerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nip")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("TradePartner");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.User.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Warehouse.WarehouseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Document.DocumentEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.DocumentType.DocumentTypeEntity", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.TradePartner.TradePartnerEntity", "TradePartner")
                        .WithMany("Documents")
                        .HasForeignKey("TradePartnerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KwiatkiBeatkiAPI.Entities.User.UserEntity", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Warehouse.WarehouseEntity", "WarehouseFrom")
                        .WithMany("DocsWithWarehousesFrom")
                        .HasForeignKey("WarehouseFromId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Warehouse.WarehouseEntity", "WarehouseTo")
                        .WithMany("DocsWithWarehousesTo")
                        .HasForeignKey("WarehouseToId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("DocumentType");

                    b.Navigation("TradePartner");

                    b.Navigation("User");

                    b.Navigation("WarehouseFrom");

                    b.Navigation("WarehouseTo");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.BulkPack.BulkPackEntity", "BulkPack")
                        .WithMany("Items")
                        .HasForeignKey("BulkPackId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("KwiatkiBeatkiAPI.Entities.ItemType.ItemTypeEntity", "ItemType")
                        .WithMany("Items")
                        .HasForeignKey("ItemTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.MeasurementUnit.MeasurementUnitEntity", "MeasurementUnit")
                        .WithMany("Items")
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Producer.ProducerEntity", "Producer")
                        .WithMany("Items")
                        .HasForeignKey("ProducerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("BulkPack");

                    b.Navigation("ItemType");

                    b.Navigation("MeasurementUnit");

                    b.Navigation("Producer");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.ItemProperty.ItemPropertyEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", "Item")
                        .WithMany("ItemProperties")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Property.PropertyEntity", "Property")
                        .WithMany("ItemProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Line.LineEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.Document.DocumentEntity", "Document")
                        .WithMany("Lines")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", "Item")
                        .WithMany("Lines")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Document");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.User.UserEntity", b =>
                {
                    b.HasOne("KwiatkiBeatkiAPI.Entities.Role.RoleEntity", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.BulkPack.BulkPackEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Document.DocumentEntity", b =>
                {
                    b.Navigation("Lines");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.DocumentType.DocumentTypeEntity", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Item.ItemEntity", b =>
                {
                    b.Navigation("ItemProperties");

                    b.Navigation("Lines");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.ItemType.ItemTypeEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.MeasurementUnit.MeasurementUnitEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Producer.ProducerEntity", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Property.PropertyEntity", b =>
                {
                    b.Navigation("ItemProperties");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Role.RoleEntity", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.TradePartner.TradePartnerEntity", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.User.UserEntity", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("KwiatkiBeatkiAPI.Entities.Warehouse.WarehouseEntity", b =>
                {
                    b.Navigation("DocsWithWarehousesFrom");

                    b.Navigation("DocsWithWarehousesTo");
                });
#pragma warning restore 612, 618
        }
    }
}
