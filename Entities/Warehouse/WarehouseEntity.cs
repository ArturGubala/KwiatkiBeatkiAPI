using KwiatkiBeatkiAPI.Entities.Document;

namespace KwiatkiBeatkiAPI.Entities.Warehouse
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual IEnumerable<DocumentEntity> DocsWithWarehousesFrom { get; } = new List<DocumentEntity>();
        public virtual IEnumerable<DocumentEntity> DocsWithWarehousesTo { get; } = new List<DocumentEntity>();
    }
}
