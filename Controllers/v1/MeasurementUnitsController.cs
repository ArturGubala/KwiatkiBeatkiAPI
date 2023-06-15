using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/measurement-units")]
    public class MeasurementUnitsController : ApiController
    {
        private readonly IMeasurementUnitsService _measurementUnitService;
        public MeasurementUnitsController(IMeasurementUnitsService measurementUnitService)
        {
            _measurementUnitService = measurementUnitService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var measurementUnitDtos = await _measurementUnitService.GetAsync();
            return Ok(measurementUnitDtos);
        }
    }
}
