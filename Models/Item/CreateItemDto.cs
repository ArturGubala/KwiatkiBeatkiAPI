using System.ComponentModel.DataAnnotations;

namespace KwiatkiBeatkiAPI.Models.Item
{
    public class CreateItemDto
    {
        [Required]
        public int ItemTypeId { get; set; }
        public int? BulkPackId { get; set; }
        public int? ProducerId { get; set; }
        [Required]
        public int MeasurementUnitId { get; set; }
        [Required]
        [MaxLength(50)]
        public string StockCode { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
        [MaxLength(50)]
        public string? Alias { get; set; }
        [MaxLength(50)]
        public string? BarCode { get; set; }
    }
}
