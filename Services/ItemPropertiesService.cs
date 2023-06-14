using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.ItemProperty;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.ItemProperty;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemPropertiesService
    {
        Task<int> CreateAsync(int itemId, CreateItemPropertyDto createItemPropertyDto);
        Task UpdateAsync(int id, UpdateItemPropertyDto updateItemPropertyDto);
        Task DeleteAsync(int id);
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
        public async Task<int> CreateAsync(int itemId, CreateItemPropertyDto createItemPropertyDto)
        {
            var itemPropertyEntity = _mapper.Map<ItemPropertyEntity>(createItemPropertyDto);
            itemPropertyEntity.ItemId = itemId;

            await _kwiatkiBeatkiDbContext.ItemProperty.AddAsync(itemPropertyEntity);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();

            return itemPropertyEntity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var itemPropertyToDelete = await _kwiatkiBeatkiDbContext.ItemProperty.FirstOrDefaultAsync(i => i.Id == id);

            if (itemPropertyToDelete == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.ItemProperty.Remove(itemPropertyToDelete);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UpdateItemPropertyDto updateItemPropertyDto)
        {
            var itemPropertyToUpdate = await _kwiatkiBeatkiDbContext.ItemProperty.FirstOrDefaultAsync(i => i.Id == id);

            if (itemPropertyToUpdate == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            itemPropertyToUpdate.Value = updateItemPropertyDto.Value;

            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }
    }
}
