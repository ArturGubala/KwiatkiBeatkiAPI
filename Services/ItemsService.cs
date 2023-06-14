using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Item;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemsService
    {
        Task<IEnumerable<ItemDto>> GetAsync();
        Task<ItemDto> GetAsync(int id);
        Task<int> CreateAsync(CreateUpdateItemDto createUpdateItemDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, CreateUpdateItemDto createUpdateItemDto);
    }
    public class ItemsService : IItemsService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public ItemsService(IMapper mapper, KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext)
        {
            _mapper = mapper;
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
        }

        public async Task<IEnumerable<ItemDto>> GetAsync()
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

        public async Task<ItemDto> GetAsync(int id)
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

        public async Task<int> CreateAsync(CreateUpdateItemDto createUpdateItemDto)
        {
            var itemEntity = _mapper.Map<ItemEntity>(createUpdateItemDto);

            _kwiatkiBeatkiDbContext.Item.Add(itemEntity);
            try
            {
                await _kwiatkiBeatkiDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException is SqlException sqlException)
                {
                    if (sqlException.Number == (short)RegexUtils.RegexTypes.CodeFromSqlExMessage)
                    {
                        string? code = string.Empty; 
                        string? value = string.Empty;
                        RegexUtils.Patterns.TryGetValue((short)RegexUtils.RegexTypes.CodeFromSqlExMessage, out Patterns? patterns);
                        if (patterns != null)
                        {
                            code = RegexUtils.GetValueByPattern(patterns.CodePattern, sqlException.Message).Split('_').Last();
                            value = RegexUtils.GetValueByPattern(patterns.ValuePattern, sqlException.Message);
                        }

                        throw new UnprocessableContentException(code, $"Pole musi być unikatowe, istnieje już wpis z wartością [{value}]", sqlException);
                    }
                }
            }

            return itemEntity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _kwiatkiBeatkiDbContext.Item.FirstOrDefaultAsync(i => i.Id == id);

            if (itemToDelete == null)
                throw new NotFoundException("ItemId", $"Item with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.Item.Remove(itemToDelete);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CreateUpdateItemDto createUpdateItemDto)
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
