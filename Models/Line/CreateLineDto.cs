using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Line
{
    public class CreateLineDto
    {
        [Required]
        public int ItemId { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        public string? Remarks { get; set; }
    }
}
