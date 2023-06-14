using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.MeasurementUnit;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IMeasurementUnitsService
    {
        Task<IEnumerable<MeasurementUnitDto>> GetAsync();
    }
    public class MeasurementUnitsService : IMeasurementUnitsService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        public MeasurementUnitsService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<MeasurementUnitDto>> GetAsync()
        {
            var measurementUnitEntities = await _kwiatkiBeatkiDbContext.MeasurementUnit.ToListAsync();
            var measurementUnitDtos = _mapper.Map<IEnumerable<MeasurementUnitDto>>(measurementUnitEntities);

            return measurementUnitDtos;
        }
    }
}
