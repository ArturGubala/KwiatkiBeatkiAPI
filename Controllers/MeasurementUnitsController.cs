using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/measurement-units")]
    [ApiController]
    [Authorize]
    public class MeasurementUnitsController : ControllerBase
    {
        private readonly IMeasurementUnitsService _measurementUnitService;
        public MeasurementUnitsController(IMeasurementUnitsService measurementUnitService)
        {
            _measurementUnitService = measurementUnitService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _measurementUnitService.GetAll();
            return Ok(items);
        }
    }
}
