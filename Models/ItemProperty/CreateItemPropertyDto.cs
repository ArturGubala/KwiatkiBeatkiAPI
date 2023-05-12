using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.ItemProperty
{
    public class CreateItemPropertyDto
    {
        public int PropertyId { get; set; }
        public string Value { get; set; }
    }
}
