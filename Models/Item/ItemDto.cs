namespace KwiatkiBeatkiAPI.Models.Item
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string ItemType{ get; set; }
        public string? BulkPack { get; set; }
        public string? Producer { get; set; }
        public string MeasurementUnit { get; set; }
        public string StockCode { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? BarCode { get; set; }
        public IEnumerable<ItemPropertyDto> ItemProperties { get; set; }
    }
}
