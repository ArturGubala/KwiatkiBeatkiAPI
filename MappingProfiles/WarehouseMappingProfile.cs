using AutoMapper;
using KwiatkiBeatkiAPI.Entities.Warehouse;
using KwiatkiBeatkiAPI.Models.Warehouse;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class WarehouseMappingProfile : Profile
    {
        public WarehouseMappingProfile()
        {
            CreateMap<WarehouseEntity, WarehouseDto>();
        }
    }
}
