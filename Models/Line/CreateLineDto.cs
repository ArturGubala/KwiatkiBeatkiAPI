namespace KwiatkiBeatkiAPI.Models.Line
{
    public class CreateLineDto
    {
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string? Remarks { get; set; }
    }
}
