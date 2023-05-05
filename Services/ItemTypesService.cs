using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.ItemType;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemTypesService
    {
        IEnumerable<ItemTypeDto> GetAll();
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
        public IEnumerable<ItemTypeDto> GetAll()
        {
            var itemTypeEntities = _kwiatkiBeatkiDbContext.ItemType.ToList();
            var itemTypeDtos = _mapper.Map<IEnumerable<ItemTypeDto>>(itemTypeEntities);

            return itemTypeDtos;
        }
    }
}
