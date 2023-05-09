using KwiatkiBeatkiAPI.Models.Line;

namespace KwiatkiBeatkiAPI.Models.Document
{
    public class CreateDocumentDto
    {
        public int DocumentTypeId { get; set; }
        public int? WarehouseFromId { get; set; }
        public int? WarehouseToId { get; set; }
        public int? TradePartnerId { get; set; }
        public IEnumerable<CreateLineDto> Lines { get; set; }
    }
}
