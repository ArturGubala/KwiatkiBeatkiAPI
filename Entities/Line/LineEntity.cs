using KwiatkiBeatkiAPI.Entities.Document;
using KwiatkiBeatkiAPI.Entities.Item;

namespace KwiatkiBeatkiAPI.Entities.Line
{
    public class LineEntity
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }
        public string? Remarks { get; set; }
        public virtual DocumentEntity Document { get; set; }
        public virtual ItemEntity Item { get; set; }
    }
}
