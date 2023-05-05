using AutoMapper;
using KwiatkiBeatkiAPI.Entities.BulkPack;
using KwiatkiBeatkiAPI.Models.BulkPack;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class BulkPackMappingProfile : Profile
    {
        public BulkPackMappingProfile()
        {
            CreateMap<BulkPackEntity, BulkPackDto>();
        }
    }
}
