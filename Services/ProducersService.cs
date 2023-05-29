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
        int CreateProducer(CreateUpdateProducerDto createUpdateProducerDto);
        void UpdateProducer(int id, CreateUpdateProducerDto createUpdateProducerDto);
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
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            var producerDto = _mapper.Map<ProducerDto>(producerEntity);

            return producerDto;
        }

        public int CreateProducer(CreateUpdateProducerDto createUpdateProducerDto)
        {
            var producerEntity = _mapper.Map<ProducerEntity>(createUpdateProducerDto);

            _kwiatkiBeatkiDbContext.Producer.Add(producerEntity);
            _kwiatkiBeatkiDbContext.SaveChanges();

            return producerEntity.Id;
        }

        public void DeleteProducer(int id)
        {
            var producerToDelete = _kwiatkiBeatkiDbContext.Producer.FirstOrDefault(i => i.Id == id);

            if (producerToDelete == null)
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.Producer.Remove(producerToDelete);
            _kwiatkiBeatkiDbContext.SaveChanges();
        }

        public void UpdateProducer(int id, CreateUpdateProducerDto createUpdateProducerDto)
        {
            var producerToUpdate = _kwiatkiBeatkiDbContext.Producer.FirstOrDefault(i => i.Id == id);

            if (producerToUpdate == null)
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            producerToUpdate.Name = createUpdateProducerDto.Name;
            producerToUpdate.PhoneNumber = createUpdateProducerDto.PhoneNumber;
            producerToUpdate.Email = createUpdateProducerDto.Email;
            producerToUpdate.Website = createUpdateProducerDto.Website;

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
