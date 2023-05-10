using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Entities.Producer;

namespace KwiatkiBeatkiAPI.Entities.Item
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public int ItemTypeId { get; set; }
        public int? BulkPackId { get; set; }
        public int? ProducerId { get; set; }
        public int MeasurementUnitId { get; set; }
        public string StockCode { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? BarCode { get; set; }
        public virtual ItemTypeEntity ItemType { get; set; }
        public virtual BulkPackEntity? BulkPack { get; set; }
        public virtual ProducerEntity? Producer { get; set; }
        public virtual MeasurementUnitEntity MeasurementUnit { get; set; }
        public virtual IEnumerable<ItemPropertyEntity> ItemProperties { get; set; }
        public virtual IEnumerable<LineEntity> Lines { get; set; }
    }
}
