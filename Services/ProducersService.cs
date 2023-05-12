using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Producer;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IProducersService
    {
        IEnumerable<ProducerDto> GetAll();
        ProducerDto GetById(int id);
        int CreateProducer(CreateProducerDto createProducerDto);
        void UpdateProducer(int id, UpdateProducerDto updateProducerDto);
        void DeleteProducer(int id);
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

        public ProducerDto GetById(int id)
        {
            var producerEntity = _kwiatkiBeatkiDbContext.Producer
                .FirstOrDefault(i => i.Id == id);

            if (producerEntity is null)
                throw new NotFoundException("Producent nie został odnaleziony.");

            var producerDto = _mapper.Map<ProducerDto>(producerEntity);

            return producerDto;
        }

        public int CreateProducer(CreateProducerDto createProducerDto)
        {
            var producerEntity = _mapper.Map<ProducerEntity>(createProducerDto);

            _kwiatkiBeatkiDbContext.Producer.Add(producerEntity);
            _kwiatkiBeatkiDbContext.SaveChanges();

            return producerEntity.Id;
        }

        public void DeleteProducer(int id)
        {
            var producerToDelete = _kwiatkiBeatkiDbContext.Producer.FirstOrDefault(i => i.Id == id);

            if (producerToDelete == null)
                throw new NotFoundException("Producent nie został odnaleziony.");

            _kwiatkiBeatkiDbContext.Producer.Remove(producerToDelete);
            _kwiatkiBeatkiDbContext.SaveChanges();
        }

        public void UpdateProducer(int id, UpdateProducerDto updateProducerDto)
        {
            var producerToUpdate = _kwiatkiBeatkiDbContext.Producer.FirstOrDefault(i => i.Id == id);

            if (producerToUpdate == null)
                throw new NotFoundException("Producent nie został odnaleziony.");

            producerToUpdate.Name = updateProducerDto.Name;
            producerToUpdate.PhoneNumber = updateProducerDto.PhoneNumber;
            producerToUpdate.Email = updateProducerDto.Email;
            producerToUpdate.Website = updateProducerDto.Website;

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
