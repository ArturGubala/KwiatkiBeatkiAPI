using KwiatkiBeatkiAPI.Entities.Item;

namespace KwiatkiBeatkiAPI.Entities.Producer
{
    public class ProducerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Website { get; set; } = null;
        public virtual IEnumerable<ItemEntity> Items { get; set; }
    }
}
