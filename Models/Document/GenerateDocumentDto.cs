using KwiatkiBeatkiAPI.Models.Line;
using KwiatkiBeatkiAPI.Models.TradePartner;
using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Models.Warehouse;

namespace KwiatkiBeatkiAPI.Models.Document
{
    public class GenerateDocumentDto
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public UserDto User { get; set; }
        public WarehouseDto? WarehouseFrom { get; set; }
        public WarehouseDto? WarehouseTo { get; set; }
        public TradePartnerDto? TradePartnerName { get; set; }
        public string FullDocumentNumber { get; set; }
        public DateTime Created { get; set; }
        public string? Remarks { get; set; }
        public IEnumerable<LineDto> Lines { get; set; }
    }
}
