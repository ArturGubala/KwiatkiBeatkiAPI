namespace KwiatkiBeatkiAPI.Models.Producer
{
    public class CreateProducerDto
    {
        public string Name { get; set; }
        public string? PhoneNumber { get; set; } = null;
        public string? Email { get; set; } = null;
        public string? Website { get; set; } = null;
    }
}
