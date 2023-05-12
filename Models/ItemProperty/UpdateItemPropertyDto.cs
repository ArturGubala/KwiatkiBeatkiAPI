using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.ItemProperty
{
    public class UpdateItemPropertyDto
    {
        [Required]
        [MaxLength(50)]
        public string Value { get; set; }
    }
}
