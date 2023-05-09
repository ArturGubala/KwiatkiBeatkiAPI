using KwiatkiBeatkiAPI.Models.Line;
using KwiatkiBeatkiAPI.Models.User;

namespace KwiatkiBeatkiAPI.Models.Document
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public UserDto User { get; set; }
        public string? WarehouseFromName { get; set; }
        public string? WarehouseToName { get; set; }
        public string? TradePartnerName { get; set; }
        public string FullDocumentNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        public IEnumerable<LineDto> Lines { get; set; }
    }
}
