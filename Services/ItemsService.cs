using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Item;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemDto>> GetAllAsync();
        Task<ItemDto> GetByIdAsync(int id);
        Task<int> CreateItemAsync(CreateUpdateItemDto createUpdateItemDto);
        Task DeleteItemAsync(int id);
        Task UpdateItemAsync(int id, CreateUpdateItemDto createUpdateItemDto);
    }
    public class ItemsService : IItemsService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public ItemsService(IUserContextService userContextService, IMapper mapper, KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            _userContextService = userContextService;
            _mapper = mapper;
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
        }

        public async Task<IEnumerable<ItemDto>> GetAllAsync()
        {
            var itemEntities = await _kwiatkiBeatkiDbContext.Item
                .Include(i => i.ItemType)
                .Include(i => i.BulkPack)
                .Include(i => i.MeasurementUnit)
                .Include(i => i.Producer)
                .Include(i => i.ItemProperties)
                    .ThenInclude(p => p.Property)
                .ToListAsync();

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(itemEntities);

            return itemDtos;
        }

        public async Task<ItemDto> GetByIdAsync(int id)
        {
            var itemEntity = await _kwiatkiBeatkiDbContext.Item
                .Include(i => i.ItemType)
                .Include(i => i.BulkPack)
                .Include(i => i.MeasurementUnit)
                .Include(i => i.Producer)
                .Include(i => i.ItemProperties)
                    .ThenInclude(x => x.Property)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (itemEntity is null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public async Task<int> CreateItemAsync(CreateUpdateItemDto createUpdateItemDto)
        {
            var itemEntity = _mapper.Map<ItemEntity>(createUpdateItemDto);

            _kwiatkiBeatkiDbContext.Item.Add(itemEntity);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();

            return itemEntity.Id;
        }

        public async Task DeleteItemAsync(int id)
        {
            var itemToDelete = await _kwiatkiBeatkiDbContext.Item.FirstOrDefaultAsync(i => i.Id == id);

            if (itemToDelete == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.Item.Remove(itemToDelete);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(int id, CreateUpdateItemDto createUpdateItemDto)
        {
            var itemToUpdate = await _kwiatkiBeatkiDbContext.Item.FirstOrDefaultAsync(i => i.Id == id);

            if (itemToUpdate == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            itemToUpdate.ItemTypeId = createUpdateItemDto.ItemTypeId;
            itemToUpdate.BulkPackId = createUpdateItemDto.BulkPackId;
            itemToUpdate.ProducerId = createUpdateItemDto.ProducerId;
            itemToUpdate.MeasurementUnitId = createUpdateItemDto.MeasurementUnitId;
            itemToUpdate.StockCode = createUpdateItemDto.StockCode;
            itemToUpdate.Name = createUpdateItemDto.Name;
            itemToUpdate.Alias = createUpdateItemDto.Alias;
            itemToUpdate.BarCode = createUpdateItemDto.BarCode;

            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }
    }
}
