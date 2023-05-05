using AutoMapper;
using KwiatkiBeatkiAPI.Entities.ItemType;
using KwiatkiBeatkiAPI.Models.ItemType;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class ItemTypeMappingProfile : Profile
    {
        public ItemTypeMappingProfile()
        {
            CreateMap<ItemTypeEntity, ItemTypeDto>();
        }
    }
}
