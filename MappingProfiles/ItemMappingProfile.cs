using AutoMapper;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Models.ItemProperty;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<ItemEntity, ItemDto>()
                .ForMember(m => m.ItemType, c => c.MapFrom(s => s.ItemType.Name))
                .ForMember(m => m.BulkPack, c => c.MapFrom(s => s.BulkPack.Name))
                .ForMember(m => m.Producer, c => c.MapFrom(s => s.Producer.Name))
                .ForMember(m => m.MeasurementUnit, c => c.MapFrom(s => s.MeasurementUnit.Abbreviation))
                .ForMember(m => m.ItemProperties, c => c.MapFrom(s => s.ItemProperties));

            CreateMap<CreateUpdateItemDto, ItemEntity>();

            CreateMap<ItemPropertyEntity, ItemPropertyDto>()
                .ForMember(m => m.Name, c => c.MapFrom(s => s.Property.Name));

            CreateMap<CreateItemPropertyDto, ItemPropertyEntity>();
        }
    }
}
