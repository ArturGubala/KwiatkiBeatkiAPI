using KwiatkiBeatkiAPI.Models.MeasurementUnit;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(typeof(IEnumerable<MeasurementUnitDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var measurementUnitDtos = await _measurementUnitService.GetAsync();
            return Ok(measurementUnitDtos);
        }
    }
}
