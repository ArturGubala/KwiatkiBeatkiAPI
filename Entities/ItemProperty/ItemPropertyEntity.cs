using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.Property;

namespace KwiatkiBeatkiAPI.Entities.ItemProperty
{
    public class ItemPropertyEntity
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
        public DateTime ModificationDate { get; set; } = DateTime.Now;
        public virtual ItemEntity Item { get; set; }
        public virtual PropertyEntity Property { get; set; }
    }
}
