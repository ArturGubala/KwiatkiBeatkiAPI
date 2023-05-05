using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/producers")]
    [ApiController]
    [Authorize]
    public class ProducersController : ControllerBase
    {
        private readonly IProducersService _producerService;
        public ProducersController(IProducersService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _producerService.GetAll();
            return Ok(items);
        }
    }
}
