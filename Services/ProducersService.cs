using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.Producer;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Producer;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IProducersService
    {
        Task<IEnumerable<ProducerDto>> GetAsync();
        Task<ProducerDto> GetAsync(int id);
        Task<int> CreateAsync(CreateUpdateProducerDto createUpdateProducerDto);
        Task UpdateAsync(int id, CreateUpdateProducerDto createUpdateProducerDto);
        Task DeleteAsync(int id);
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
        public async Task<IEnumerable<ProducerDto>> GetAsync()
        {
            var producerEntities = await _kwiatkiBeatkiDbContext.Producer.ToListAsync();
            var producerDtos = _mapper.Map<IEnumerable<ProducerDto>>(producerEntities);

            return producerDtos;
        }

        public async Task<ProducerDto> GetAsync(int id)
        {
            var producerEntity = await _kwiatkiBeatkiDbContext.Producer
                .FirstOrDefaultAsync(i => i.Id == id);

            if (producerEntity is null)
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            var producerDto = _mapper.Map<ProducerDto>(producerEntity);

            return producerDto;
        }

        public async Task<int> CreateAsync(CreateUpdateProducerDto createUpdateProducerDto)
        {
            var producerEntity = _mapper.Map<ProducerEntity>(createUpdateProducerDto);

            await _kwiatkiBeatkiDbContext.Producer.AddAsync(producerEntity);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();

            return producerEntity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var producerToDelete = await _kwiatkiBeatkiDbContext.Producer.FirstOrDefaultAsync(i => i.Id == id);

            if (producerToDelete == null)
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            _kwiatkiBeatkiDbContext.Producer.Remove(producerToDelete);
            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CreateUpdateProducerDto createUpdateProducerDto)
        {
            var producerToUpdate = await _kwiatkiBeatkiDbContext.Producer.FirstOrDefaultAsync(i => i.Id == id);

            if (producerToUpdate == null)
                throw new NotFoundException("ProducerId", $"Producer with ID: {id} was not found");

            producerToUpdate.Name = createUpdateProducerDto.Name;
            producerToUpdate.PhoneNumber = createUpdateProducerDto.PhoneNumber;
            producerToUpdate.Email = createUpdateProducerDto.Email;
            producerToUpdate.Website = createUpdateProducerDto.Website;

            await _kwiatkiBeatkiDbContext.SaveChangesAsync();
        }
    }
}
