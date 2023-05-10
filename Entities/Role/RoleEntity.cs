using KwiatkiBeatkiAPI.Entities.User;

namespace KwiatkiBeatkiAPI.Entities.Role
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<UserEntity> Users { get; set; }
    }
}
