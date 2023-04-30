using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Item;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IItemsService
    {
        IEnumerable<ItemDto> GetAll();
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
                    .ThenInclude(x => x.Property)
                .ToList();

            var itemDto = _mapper.Map<IEnumerable<ItemDto>>(itemEntities);

            return itemDto;
        }
    }
}
