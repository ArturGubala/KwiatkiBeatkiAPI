﻿using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Entities.Document;
using KwiatkiBeatkiAPI.Entities.DocumentType;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Entities.Property;
using KwiatkiBeatkiAPI.Entities.Role;
using KwiatkiBeatkiAPI.Entities.TradePartner;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Warehouse;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface ISeederService
    {
        void Seed();
    }
    public class SeederService : ISeederService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;

        public SeederService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IPasswordHasher<UserEntity> passwordHasher)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (!_kwiatkiBeatkiDbContext.Database.CanConnect())
                return;

            var pendingMigrations = _kwiatkiBeatkiDbContext.Database.GetPendingMigrations();
            if (pendingMigrations != null && pendingMigrations.Any())
                _kwiatkiBeatkiDbContext.Database.Migrate();

            if (!_kwiatkiBeatkiDbContext.Role.Any())
            {
                var roles = GetRoles();
                _kwiatkiBeatkiDbContext.Role.AddRange(roles);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.User.Any())
            {
                var users = GetUsers();
                _kwiatkiBeatkiDbContext.User.AddRange(users);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.MeasurementUnit.Any())
            {
                var measurementUnits = GetMeasurementUnits();
                _kwiatkiBeatkiDbContext.MeasurementUnit.AddRange(measurementUnits);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.ItemType.Any())
            {
                var itemTypes = GetItemTypes();
                _kwiatkiBeatkiDbContext.ItemType.AddRange(itemTypes);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.BulkPack.Any())
            {
                var bulkPacks = GetBulkPacks();
                _kwiatkiBeatkiDbContext.BulkPack.AddRange(bulkPacks);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Producer.Any())
            {
                var producers = GetProducers();
                _kwiatkiBeatkiDbContext.Producer.AddRange(producers);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Property.Any())
            {
                var properties = GetProperties();
                _kwiatkiBeatkiDbContext.Property.AddRange(properties);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Item.Any())
            {
                var items = GetItems();
                _kwiatkiBeatkiDbContext.Item.AddRange(items);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.ItemProperty.Any())
            {
                var itemProperties = GetItemProperties();
                _kwiatkiBeatkiDbContext.ItemProperty.AddRange(itemProperties);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Warehouse.Any())
            {
                var warehouses = GetWarehouseEntities();
                _kwiatkiBeatkiDbContext.Warehouse.AddRange(warehouses);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.DocumentType.Any())
            {
                var documentTypes = GetDocumentTypes();
                _kwiatkiBeatkiDbContext.DocumentType.AddRange(documentTypes);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.TradePartner.Any())
            {
                var tradePartners = GetTradePartners();
                _kwiatkiBeatkiDbContext.TradePartner.AddRange(tradePartners);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Document.Any())
            {
                var documents = GetDocuments();
                _kwiatkiBeatkiDbContext.Document.AddRange(documents);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }

            if (!_kwiatkiBeatkiDbContext.Line.Any())
            {
                var lines = GetLines();
                _kwiatkiBeatkiDbContext.Line.AddRange(lines);
                _kwiatkiBeatkiDbContext.SaveChanges();
            }
        }

        private IEnumerable<RoleEntity> GetRoles()
        {
            var roles = new List<RoleEntity>()
            {
                new RoleEntity()
                {
                    Name = "Admin",
                },
                new RoleEntity()
                {
                    Name = "Menager",
                },
                new RoleEntity()
                {
                    Name = "User",
                }
            };

            return roles;
        }

        private IEnumerable<UserEntity> GetUsers()
        {
            string hashedPassword;
            var users = new List<UserEntity>();

            var admin = new UserEntity()
            {
                Email = "admin@kwiatkibeatki.pl",
                FirstName = "Admin",
                LastName = "Superuser",
                RoleId = 1
            };
            hashedPassword = _passwordHasher.HashPassword(admin, "admin123");
            admin.PasswordHash = hashedPassword;

            var menager = new UserEntity()
            {
                Email = "menager@kwiatkibeatki.pl",
                FirstName = "Menager",
                RoleId = 2
            };
            hashedPassword = _passwordHasher.HashPassword(menager, "menager123");
            menager.PasswordHash = hashedPassword;

            var user = new UserEntity()
            {
                Email = "user@kwiatkibeatki.pl",
                FirstName = "User",
                LastName = "JustUser",
                RoleId = 3
            };
            hashedPassword = _passwordHasher.HashPassword(user, "user123");
            user.PasswordHash = hashedPassword;

            users.Add(admin);
            users.Add(menager);
            users.Add(user);

            return users;
        }

        private IEnumerable<MeasurementUnitEntity> GetMeasurementUnits()
        {
            var measurementUnits = new List<MeasurementUnitEntity>()
            {
                new MeasurementUnitEntity()
                {
                    Name = "kilogram",
                    Abbreviation = "kg"
                },
                new MeasurementUnitEntity()
                {
                    Name = "sztuki",
                    Abbreviation = "szt"
                }
            };

            return measurementUnits;
        }

        private IEnumerable<ItemTypeEntity> GetItemTypes()
        {
            var itemTypes = new List<ItemTypeEntity>()
            {
                new ItemTypeEntity()
                {
                    Name = "wkład",
                },
                new ItemTypeEntity()
                {
                    Name = "znicz"
                },
                new ItemTypeEntity()
                {
                    Name = "wkład elektryczny"
                }
            };

            return itemTypes;
        }

        private IEnumerable<BulkPackEntity> GetBulkPacks()
        {
            var bulkPacks = new List<BulkPackEntity>()
            {
                new BulkPackEntity()
                {
                    Name = "zgrzewka",
                    Abbreviation = "zg."
                },
                new BulkPackEntity()
                {
                    Name = "karton",
                    Abbreviation = "kart."
                },
                new BulkPackEntity()
                {
                    Name = "display",
                    Abbreviation = "dspl."
                }
            };

            return bulkPacks;
        }

        private IEnumerable<ProducerEntity> GetProducers() 
        {
            var producers = new List<ProducerEntity>()
            {
                new ProducerEntity()
                {
                    Name = "Admar",
                    PhoneNumber = "447234964",
                    Email = "admarmz@wp.pl",
                    Website = "https://www.zniczeadmar.pl"
                },
                new ProducerEntity()
                {
                    Name = "Płomyk",
                    PhoneNumber = "627414579",
                    Email = "pph-plomyk@pph-plomyk.pl",
                    Website = "https://www.pph-plomyk.pl"
                },
                new ProducerEntity()
                {
                    Name = "Ignis"
                },
                new ProducerEntity()
                {
                    Name = "Assai",
                    PhoneNumber = "Assai",
                    Email = "handlowy@assai.com.pl",
                    Website = "https://subito.pl/"
                }
            };

            return producers;
        }

        private IEnumerable<PropertyEntity> GetProperties()
        {
            var properties = new List<PropertyEntity>()
            {
                new PropertyEntity()
                {
                    Name = "Wysokość (cm)"
                },
                new PropertyEntity()
                {
                    Name = "Szerokość (cm)"
                },
                new PropertyEntity()
                {
                    Name = "Średnica (cm)"
                },
                new PropertyEntity()
                {
                    Name = "Czas palenia (h)"
                },
                new PropertyEntity()
                {
                    Name = "Rodzaj wypełnienia"
                },
                new PropertyEntity()
                {
                    Name = "Zasilanie"
                },
                new PropertyEntity()
                {
                    Name = "Ilość szt. w opakowaniu jednostkowym"
                },
                new PropertyEntity()
                {
                    Name = "Ilość szt. na warstwie palety"
                },
                new PropertyEntity()
                {
                    Name = "Ilość szt. na palecie"
                },
                new PropertyEntity()
                {
                    Name = "Ilość opakowań na warstwie palety"
                },
                new PropertyEntity()
                {
                    Name = "Ilość opakowań na palecie"
                },
                new PropertyEntity()
                {
                    Name = "Ilość szt. w opakowaniu zbiorczym"
                },
            };

            return properties;
        }

        private IEnumerable<ItemEntity> GetItems()
        {
            var items = new List<ItemEntity>()
            {
                new ItemEntity()
                {
                    ItemTypeId = 1,
                    BulkPackId = 1,
                    ProducerId = 3,
                    MeasurementUnitId = 2,
                    StockCode = "PROMYK 2",
                },
                new ItemEntity()
                {
                    ItemTypeId = 2,
                    BulkPackId = 1,
                    ProducerId = 3,
                    MeasurementUnitId = 2,
                    StockCode = "ZP 37 BP",
                    Alias = "Serce lane średnie"
                },
                new ItemEntity()
                {
                    ItemTypeId = 2,
                    BulkPackId = 1,
                    ProducerId = 3,
                    MeasurementUnitId = 2,
                    StockCode = "ZP 22A",
                    Alias = "Kapcerek"

                },
                new ItemEntity()
                {
                    ItemTypeId = 1,
                    BulkPackId = 1,
                    ProducerId = 1,
                    MeasurementUnitId = 2,
                    StockCode = "FUEGO 2",
                },
                new ItemEntity()
                {
                    ItemTypeId = 2,
                    BulkPackId = 2,
                    ProducerId = 3,
                    MeasurementUnitId = 2,
                    StockCode = "GLASS ART TRAPEZ 3",
                },
                new ItemEntity()
                {
                    ItemTypeId = 3,
                    BulkPackId = 3,
                    ProducerId = 4,
                    MeasurementUnitId = 2,
                    StockCode = "SUBITO S1",
                },
            };

            return items;
        }

        private IEnumerable<ItemPropertyEntity> GetItemProperties()
        {
            var items = new List<ItemPropertyEntity>()
            {
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 1,
                    Value = "14"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 3,
                    Value = "5.5"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 4,
                    Value = "50"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 7,
                    Value = "15"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 8,
                    Value = "240"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 1,
                    PropertyId = 10,
                    Value = "16"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 2,
                    PropertyId = 7,
                    Value = "12"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 2,
                    PropertyId = 8,
                    Value = "96"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 2,
                    PropertyId = 10,
                    Value = "8"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 3,
                    PropertyId = 7,
                    Value = "16"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 3,
                    PropertyId = 8,
                    Value = "128"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 3,
                    PropertyId = 10,
                    Value = "8"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 4,
                    PropertyId = 1,
                    Value = "12"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 4,
                    PropertyId = 3,
                    Value = "5.5"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 4,
                    PropertyId = 4,
                    Value = "50"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 4,
                    PropertyId = 7,
                    Value = "34"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 5,
                    PropertyId = 7,
                    Value = "6"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 1,
                    Value = "10.8"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 3,
                    Value = "4.6"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 4,
                    Value = "1200"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 6,
                    Value = "2x R6 (AA)"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 7,
                    Value = "12"
                },
                new ItemPropertyEntity()
                {
                    ItemId = 6,
                    PropertyId = 12,
                    Value = "192"
                },
            };

            return items;
        }

        private IEnumerable<WarehouseEntity> GetWarehouseEntities()
        {
            var warehouses = new List<WarehouseEntity>()
            { 
                new WarehouseEntity() 
                {
                    Name = "Sklep Kwiatki Beatki",
                    Code = "SKB"
                },
                new WarehouseEntity()
                {
                    Name = "Stoisko Dębowa Bus",
                    Code = "SDB"
                },
                new WarehouseEntity()
                {
                    Name = "Stoisko Dębowa Przyczepa",
                    Code = "SDP"
                },
                new WarehouseEntity()
                {
                    Name = "Stoisko Limanowskiego Paulina",
                    Code = "SLP"
                }
            };

            return warehouses;
        }

        private IEnumerable<DocumentTypeEntity> GetDocumentTypes()
        {
            var documentTypes = new List<DocumentTypeEntity>()
            {
                new DocumentTypeEntity()
                {
                    Name = "Zamówienie do dostawcy",
                    Abbreviation = "ZAMDOST"
                },
                new DocumentTypeEntity()
                {
                    Name = "Przesunięcie między magazynowe",
                    Abbreviation = "MM"
                }
            };

            return documentTypes;
        }

        private IEnumerable<TradePartnerEntity> GetTradePartners()
        {
            var tradePartners = new List<TradePartnerEntity>()
            {
                new TradePartnerEntity()
                {
                    Name = "Hurtownia Zniczy i Świec Płomyk",
                    Email = "kkhajduk@op.pl",
                    PhoneNumber = "607077661",
                    Website = "https://znicze.radom.pl",
                    Street = "Lubelska",
                    StreetNumber = "65",
                    City = "Radom",
                    PostalCode = "26600",
                }
            };

            return tradePartners;
        }

        private IEnumerable<DocumentEntity> GetDocuments()
        {
            var documents = new List<DocumentEntity>()
            {
                new DocumentEntity()
                {
                    DocumentTypeId = 1,
                    UserId = 1,
                    WarehouseFromId = null,
                    WarehouseToId = null,
                    TradePartnerId = 1,
                    FullDocumentNumber = "01/ZAMDOST/05/2023",
                    DocumentNumber = 1,
                    Remarks = "Przykładowe uwagi do dokumentu. Mieści się tu dużo znaków, aż czysta hm 300."
                },
                new DocumentEntity()
                {
                    DocumentTypeId = 1,
                    UserId = 2,
                    WarehouseFromId = null,
                    WarehouseToId = null,
                    TradePartnerId = 1,
                    FullDocumentNumber = "02/ZAMDOST/05/2023",
                    DocumentNumber = 2,
                    Remarks = ""
                },
                new DocumentEntity()
                {
                    DocumentTypeId = 2,
                    UserId = 3,
                    WarehouseFromId = 1,
                    WarehouseToId = 4,
                    TradePartnerId = null,
                    FullDocumentNumber = "01/MM/05/2023",
                    DocumentNumber = 1,
                    Remarks = ""
                },
                new DocumentEntity()
                {
                    DocumentTypeId = 2,
                    UserId = 2,
                    WarehouseFromId = 2,
                    WarehouseToId = 3,
                    TradePartnerId = null,
                    FullDocumentNumber = "02/MM/05/2023",
                    DocumentNumber = 2,
                    Remarks = "Uwagi do dokumentu MM. Następnym razem proszę trochę szybciej!"
                }
            };

            return documents;
        }

        private IEnumerable<LineEntity> GetLines()
        {
            var lines = new List<LineEntity>()
            {
                new LineEntity()
                {
                    DocumentId = 1,
                    ItemId = 1,
                    Quantity = 720,
                    Remarks = "Poproszę na osobnej palecie"
                },
                new LineEntity()
                {
                    DocumentId = 1,
                    ItemId = 2,
                    Quantity = 48
                },
                new LineEntity()
                {
                    DocumentId = 2,
                    ItemId = 2,
                    Quantity = 96
                },
                new LineEntity()
                {
                    DocumentId = 2,
                    ItemId = 3,
                    Quantity = 128
                },
                new LineEntity()
                {
                    DocumentId = 2,
                    ItemId = 5,
                    Quantity = 18,
                    Remarks = "Po kartonie z kolorów: złoty, srebny i reflex"
                },
                new LineEntity()
                {
                    DocumentId = 3,
                    ItemId = 1,
                    Quantity = 120,
                },
                new LineEntity()
                {
                    DocumentId = 3,
                    ItemId = 5,
                    Quantity = 68,
                },
                new LineEntity()
                {
                    DocumentId = 4,
                    ItemId = 6,
                    Quantity = 36,
                    Remarks = "Po displayu z kolorów: biały, czerwony, żółty"
                },
            };

            return lines;
        }
    }
}
