using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Models.ItemProperty;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemPropertiesService
    {
        int CreateItemProperty(int itemId, CreateItemPropertyDto createItemPropertyDto);
        void UpdateItemProperty(int id, UpdateItemPropertyDto updateItemPropertyDto);
        void DeleteItemProperty(int id);
    }
    public class ItemPropertiesService : IItemPropertiesService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public ItemPropertiesService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }
        public int CreateItemProperty(int itemId, CreateItemPropertyDto createItemPropertyDto)
        {
            var itemPropertyEntity = _mapper.Map<ItemPropertyEntity>(createItemPropertyDto);
            itemPropertyEntity.ItemId = itemId;

            _kwiatkiBeatkiDbContext.ItemProperty.Add(itemPropertyEntity);
            _kwiatkiBeatkiDbContext.SaveChanges();

            return itemPropertyEntity.Id;
        }

        public void DeleteItemProperty(int id)
        {
            var itemPropertyToDelete = _kwiatkiBeatkiDbContext.ItemProperty.FirstOrDefault(i => i.Id == id);

            if (itemPropertyToDelete == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.ItemProperty.Remove(itemPropertyToDelete);
            _kwiatkiBeatkiDbContext.SaveChanges();
        }

        public void UpdateItemProperty(int id, UpdateItemPropertyDto updateItemPropertyDto)
        {
            var itemPropertyToUpdate = _kwiatkiBeatkiDbContext.ItemProperty.FirstOrDefault(i => i.Id == id);

            if (itemPropertyToUpdate == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            itemPropertyToUpdate.Value = updateItemPropertyDto.Value;

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
