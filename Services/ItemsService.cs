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
        IEnumerable<ItemDto> GetAll();
        ItemDto GetById(int id);
        int CreateItem(CreateItemDto createItemDto);
        void DeleteItem(int id);
        void UpdateItem(int id, UpdateItemDto createUpdateItemDto);
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

        public IEnumerable<ItemDto> GetAll()
        {
            var itemEntities = _kwiatkiBeatkiDbContext.Item
                .Include(i => i.ItemType)
                .Include(i => i.BulkPack)
                .Include(i => i.MeasurementUnit)
                .Include(i => i.Producer)
                .Include(i => i.ItemProperties)
                    .ThenInclude(p => p.Property)
                .ToList();

            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(itemEntities);

            return itemDtos;
        }

        public ItemDto GetById(int id)
        {
            var itemEntity = _kwiatkiBeatkiDbContext.Item
                .Include(i => i.ItemType)
                .Include(i => i.BulkPack)
                .Include(i => i.MeasurementUnit)
                .Include(i => i.Producer)
                .Include(i => i.ItemProperties)
                    .ThenInclude(x => x.Property)
                .FirstOrDefault(i => i.Id == id);

            if (itemEntity is null)
                throw new NotFoundException("Item not found");

            var itemDto = _mapper.Map<ItemDto>(itemEntity);

            return itemDto;
        }

        public int CreateItem(CreateItemDto createItemDto)
        {
            var itemEntity = _mapper.Map<ItemEntity>(createItemDto);

            _kwiatkiBeatkiDbContext.Item.Add(itemEntity);
            _kwiatkiBeatkiDbContext.SaveChanges();

            return itemEntity.Id;
        }

        public void DeleteItem(int id)
        {
            var itemToDelete = _kwiatkiBeatkiDbContext.Item.FirstOrDefault(i => i.Id == id);

            if (itemToDelete == null)
                throw new NotFoundException("Item not found");

            _kwiatkiBeatkiDbContext.Item.Remove(itemToDelete);
            _kwiatkiBeatkiDbContext.SaveChanges();
        }

        public void UpdateItem(int id, UpdateItemDto updateItemDto)
        {
            var itemToUpdate = _kwiatkiBeatkiDbContext.Item.FirstOrDefault(i => i.Id == id);

            if (itemToUpdate == null)
                throw new NotFoundException("Item not found");

            itemToUpdate.ItemTypeId = updateItemDto.ItemTypeId;
            itemToUpdate.BulkPackId = updateItemDto.BulkPackId;
            itemToUpdate.ProducerId = updateItemDto.ProducerId;
            itemToUpdate.MeasurementUnitId = updateItemDto.MeasurementUnitId;
            itemToUpdate.StockCode = updateItemDto.StockCode;
            itemToUpdate.Name = updateItemDto.Name;
            itemToUpdate.Alias = updateItemDto.Alias;
            itemToUpdate.BarCode = updateItemDto.BarCode;

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
