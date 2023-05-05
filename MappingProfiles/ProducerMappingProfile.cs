using AutoMapper;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Models.Producer;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class ProducerMappingProfile : Profile
    {
        public ProducerMappingProfile()
        {
            CreateMap<ProducerEntity, ProducerDto>();
        }
    }
}
