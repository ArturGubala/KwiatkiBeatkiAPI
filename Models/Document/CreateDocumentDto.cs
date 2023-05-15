using KwiatkiBeatkiAPI.Models.Line;
using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Document
{
    public class CreateDocumentDto
    {
        [Required]
        public int DocumentTypeId { get; set; }
        public int? WarehouseFromId { get; set; }
        public int? WarehouseToId { get; set; }
        public int? TradePartnerId { get; set; }
        [Required]
        public IEnumerable<CreateLineDto> Lines { get; set; }
    }
}
