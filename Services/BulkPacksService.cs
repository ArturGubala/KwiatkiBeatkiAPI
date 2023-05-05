using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.BulkPack;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IBulkPacksService
    {
        IEnumerable<BulkPackDto> GetAll();
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
        public IEnumerable<BulkPackDto> GetAll()
        {
            var bulkPacksEntities = _kwiatkiBeatkiDbContext.BulkPack.ToList();
            var bulkPacksDtos = _mapper.Map<IEnumerable<BulkPackDto>>(bulkPacksEntities);

            return bulkPacksDtos;
        }
    }
}
