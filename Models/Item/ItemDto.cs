using KwiatkiBeatkiAPI.Models.BulkPack;
using KwiatkiBeatkiAPI.Models.ItemProperty;
using KwiatkiBeatkiAPI.Models.ItemType;
using KwiatkiBeatkiAPI.Models.MeasurementUnit;
using KwiatkiBeatkiAPI.Models.Producer;

namespace KwiatkiBeatkiAPI.Models.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public ItemTypeDto ItemType{ get; set; }
        public BulkPackDto? BulkPack { get; set; }
        public ProducerDto? Producer { get; set; }
        public MeasurementUnitDto MeasurementUnit { get; set; }
        public string StockCode { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? BarCode { get; set; }
        public IEnumerable<ItemPropertyDto> ItemProperties { get; set; }
    }
}
