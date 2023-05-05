namespace KwiatkiBeatkiAPI.Models.Item
{
    public class CreateItemDto
    {
        public int ItemTypeId { get; set; }
        public int? BulkPackId { get; set; }
        public int? ProducerId { get; set; }
        public int MeasurementUnitId { get; set; }
        public string StockCode { get; set; }
        public string? Name { get; set; }
        public string? Alias { get; set; }
        public string? BarCode { get; set; }
    }
}
