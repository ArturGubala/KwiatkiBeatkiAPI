using KwiatkiBeatkiAPI.Entities.Document;

namespace KwiatkiBeatkiAPI.Entities.TradePartner
{
    public class TradePartnerEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Website { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Nip { get; set; }
        public virtual IEnumerable<DocumentEntity> Documents { get; set; }
    }
}
