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
        public async Task<IActionResult> Get()
        {
            var producerDtos = await _producerService.GetAsync();
            return Ok(producerDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var producerDto = await _producerService.GetAsync(id);
            return Ok(producerDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            var createdProducerId = await _producerService.CreateAsync(createUpdateProducerDto);
            return Created($"api/v1/producers/{createdProducerId}", null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _producerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            await _producerService.UpdateAsync(id, createUpdateProducerDto);
            return NoContent();
        }
    }
}
