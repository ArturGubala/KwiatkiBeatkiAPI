using KwiatkiBeatkiAPI.Entities.DocumentType;
using KwiatkiBeatkiAPI.Entities.Line;
using KwiatkiBeatkiAPI.Entities.TradePartner;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Entities.Warehouse;
using System.ComponentModel.DataAnnotations.Schema;

namespace KwiatkiBeatkiAPI.Entities.Document
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public int DocumentTypeId { get; set; }
        public int UserId { get; set; }
        public int? WarehouseFromId { get; set; }
        public int? WarehouseToId { get; set; }
        public int? TradePartnerId { get; set; }
        public string FullDocumentNumber { get; set; }
        public int DocumentNumber { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        public virtual DocumentTypeEntity DocumentType { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual WarehouseEntity? WarehouseFrom { get; set; }
        public virtual WarehouseEntity? WarehouseTo { get; set; }
        public virtual TradePartnerEntity? TradePartner { get; set; }
        public virtual IEnumerable<LineEntity> Lines { get; set; }
    }
}
