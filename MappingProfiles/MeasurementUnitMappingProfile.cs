using AutoMapper;
using KwiatkiBeatkiAPI.Entities.MeasurementUnit;
using KwiatkiBeatkiAPI.Models.MeasurementUnit;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class MeasurementUnitMappingProfile : Profile
    {
        public MeasurementUnitMappingProfile() 
        {
        CreateMap<MeasurementUnitEntity, MeasurementUnitDto>();
        }
    }
}
