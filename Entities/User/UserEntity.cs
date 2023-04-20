using KwiatkiBeatkiAPI.Entities.Role;

namespace KwiatkiBeatkiAPI.Entities.User
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }
        public virtual RoleEntity Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
