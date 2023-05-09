using KwiatkiBeatkiAPI.Entities.Document;

namespace KwiatkiBeatkiAPI.Entities.Warehouse
{
    public class WarehouseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual IEnumerable<DocumentEntity> WarehousesFrom { get; } = new List<DocumentEntity>();
        public virtual IEnumerable<DocumentEntity> WarehousesTo { get; } = new List<DocumentEntity>();
    }
}
