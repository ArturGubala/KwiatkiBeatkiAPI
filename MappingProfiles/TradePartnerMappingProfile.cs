using AutoMapper;
using KwiatkiBeatkiAPI.Entities.TradePartner;
using KwiatkiBeatkiAPI.Models.TradePartner;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class TradePartnerMappingProfile : Profile
    {
        public TradePartnerMappingProfile()
        {
            CreateMap<TradePartnerEntity, TradePartnerDto>();
        }
    }
}
