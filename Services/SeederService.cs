using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Role;
using KwiatkiBeatkiAPI.Entities.User;
using Microsoft.AspNetCore.Identity;

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
        
    }
}
