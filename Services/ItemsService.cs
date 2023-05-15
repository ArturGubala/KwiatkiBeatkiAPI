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
        int CreateItem(CreateUpdateItemDto createUpdateItemDto);
        void DeleteItem(int id);
        void UpdateItem(int id, CreateUpdateItemDto createUpdateItemDto);
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

        public int CreateItem(CreateUpdateItemDto createUpdateItemDto)
        {
            var itemEntity = _mapper.Map<ItemEntity>(createUpdateItemDto);

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

        public void UpdateItem(int id, CreateUpdateItemDto createUpdateItemDto)
        {
            var itemToUpdate = _kwiatkiBeatkiDbContext.Item.FirstOrDefault(i => i.Id == id);

            if (itemToUpdate == null)
                throw new NotFoundException("Item not found");

            itemToUpdate.ItemTypeId = createUpdateItemDto.ItemTypeId;
            itemToUpdate.BulkPackId = createUpdateItemDto.BulkPackId;
            itemToUpdate.ProducerId = createUpdateItemDto.ProducerId;
            itemToUpdate.MeasurementUnitId = createUpdateItemDto.MeasurementUnitId;
            itemToUpdate.StockCode = createUpdateItemDto.StockCode;
            itemToUpdate.Name = createUpdateItemDto.Name;
            itemToUpdate.Alias = createUpdateItemDto.Alias;
            itemToUpdate.BarCode = createUpdateItemDto.BarCode;

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
