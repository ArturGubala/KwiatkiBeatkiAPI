using KwiatkiBeatkiAPI.Entities.Item;

namespace KwiatkiBeatkiAPI.Entities.ItemType
{
    public class ItemTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<ItemEntity> Items { get; set; }
    }
}
