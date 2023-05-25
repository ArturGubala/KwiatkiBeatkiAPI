using KwiatkiBeatkiAPI.Models.Producer;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/producers")]
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
            var producerDtos = _producerService.GetAll();
            return Ok(producerDtos);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get([FromRoute] int id)
        {
            var producerDto = _producerService.GetById(id);
            return Ok(producerDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            var createdProducerId = _producerService.CreateProducer(createUpdateProducerDto);
            return Created($"api/v1/producers/{createdProducerId}", null);
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
