using KwiatkiBeatkiAPI.Models.Producer;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/producers")]
    public class ProducersController : ApiController
    {
        private readonly IProducersService _producerService;
        public ProducersController(IProducersService producerService)
        {
            _producerService = producerService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProducerDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var producerDtos = await _producerService.GetAsync();
            return Ok(producerDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProducerDto), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var producerDto = await _producerService.GetAsync(id);
            return Ok(producerDto);
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Post([FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            var createdProducerId = await _producerService.CreateAsync(createUpdateProducerDto);
            var createdResourceUrl = Url.Action(nameof(Get), new { id = createdProducerId });
            return Created(createdResourceUrl!, null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Delete(int id)
        {
            await _producerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CreateUpdateProducerDto createUpdateProducerDto)
        {
            await _producerService.UpdateAsync(id, createUpdateProducerDto);
            return NoContent();
        }
    }
}
