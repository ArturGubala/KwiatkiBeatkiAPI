using KwiatkiBeatkiAPI.Entities.Item;

namespace KwiatkiBeatkiAPI.Entities.MeasurementUnit
{
    public class MeasurementUnitEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public virtual IEnumerable<ItemEntity> Items { get; set; }
    }
}
