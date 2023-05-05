using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Models.MeasurementUnit;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IMeasurementUnitsService
    {
        IEnumerable<MeasurementUnitDto> GetAll();
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
        public IEnumerable<MeasurementUnitDto> GetAll()
        {
            var measurementUnitEntities = _kwiatkiBeatkiDbContext.MeasurementUnit.ToList();
            var measurementUnitDtos = _mapper.Map<IEnumerable<MeasurementUnitDto>>(measurementUnitEntities);

            return measurementUnitDtos;
        }
    }
}
