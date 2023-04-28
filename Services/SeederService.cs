using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Entities.Property;
using KwiatkiBeatkiAPI.Entities.Role;
using KwiatkiBeatkiAPI.Entities.User;
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
                    Abbreviation = "zg"
                },
                new BulkPackEntity()
                {
                    Name = "karton",
                    Abbreviation = "kart"
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
                    Name = "Wysokość"
                },
                new PropertyEntity()
                {
                    Name = "Szerokość"
                },
                new PropertyEntity()
                {
                    Name = "Średnica"
                },
                new PropertyEntity()
                {
                    Name = "Czas palenia"
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
                    Name = "Ilość w opakowaniu"
                },
                new PropertyEntity()
                {
                    Name = "Ilość na warstwie palety"
                },
                new PropertyEntity()
                {
                    Name = "Ilość na palecie"
                },
                new PropertyEntity()
                {
                    Name = "Ilość opakowań na warstwie palety"
                },
                new PropertyEntity()
                {
                    Name = "Ilość opakowań na palecie"
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
                    BulkPackId = 1,
                    ProducerId = 4,
                    MeasurementUnitId = 2,
                    StockCode = "SUBITO S1",
                },
            };

            return items;
        }
    }
}
