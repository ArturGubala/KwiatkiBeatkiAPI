namespace KwiatkiBeatkiAPI.Models.Line
{
    public class LineDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string StockCode { get; set; }
        public string? Alias { get; set; }
        public decimal Quantity { get; set; }
        public string MeasurementUnitAbbrev { get; set; }
        public string? Remarks { get; set; }
    }
}
