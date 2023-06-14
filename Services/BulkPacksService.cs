using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.BulkPack;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IBulkPacksService
    {
        Task<IEnumerable<BulkPackDto>> GetAsync();
    }
    public class BulkPacksService : IBulkPacksService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public BulkPacksService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BulkPackDto>> GetAsync()
        {
            var bulkPacksEntities = await _kwiatkiBeatkiDbContext.BulkPack.ToListAsync();
            var bulkPacksDtos = _mapper.Map<IEnumerable<BulkPackDto>>(bulkPacksEntities);

            return bulkPacksDtos;
        }
    }
}
