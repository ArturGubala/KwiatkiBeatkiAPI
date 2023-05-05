using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.Producer;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IProducersService
    {
        IEnumerable<ProducerDto> GetAll();
    }
    public class ProducersService : IProducersService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public ProducersService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }
        public IEnumerable<ProducerDto> GetAll()
        {
            var producerEntities = _kwiatkiBeatkiDbContext.Producer.ToList();
            var producerDtos = _mapper.Map<IEnumerable<ProducerDto>>(producerEntities);

            return producerDtos;
        }
    }
}
