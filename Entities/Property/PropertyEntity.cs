using KwiatkiBeatkiAPI.Entities.ItemProperty;

namespace KwiatkiBeatkiAPI.Entities.Property
{
    public class PropertyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IEnumerable<ItemPropertyEntity> ItemProperties { get; set; }
    }
}
