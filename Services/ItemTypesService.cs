using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.ItemType;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemTypesService
    {
        Task<IEnumerable<ItemTypeDto>> GetAsync();
    }
    public class ItemTypesService : IItemTypesService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public ItemTypesService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ItemTypeDto>> GetAsync()
        {
            var itemTypeEntities = await _kwiatkiBeatkiDbContext.ItemType.ToListAsync();
            var itemTypeDtos = _mapper.Map<IEnumerable<ItemTypeDto>>(itemTypeEntities);

            return itemTypeDtos;
        }
    }
}
