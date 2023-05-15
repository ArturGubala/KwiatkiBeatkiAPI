using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Document
{
    public class UpdateDocumentDto
    {
        [Required]
        public int Id { get; set; }
        public string Remarks { get; set; }
    }
}
