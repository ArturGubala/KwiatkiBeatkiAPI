using KwiatkiBeatkiAPI.Models.Producer;
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

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var producer = _producerService.GetById(id);
            return Ok(producer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            var createdProducerId = _producerService.CreateProducer(createUpdateProducerDto);
            return Created($"api/producers/{createdProducerId}", null);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _producerService.DeleteProducer(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            _producerService.UpdateProducer(id, createUpdateProducerDto);
            return NoContent();
        }
    }
}
