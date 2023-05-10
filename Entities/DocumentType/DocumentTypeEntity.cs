using KwiatkiBeatkiAPI.Controllers;
using KwiatkiBeatkiAPI.Entities.Document;

namespace KwiatkiBeatkiAPI.Entities.DocumentType
{
    public class DocumentTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public virtual IEnumerable<DocumentEntity> Documents { get; set; } 
    }
}
