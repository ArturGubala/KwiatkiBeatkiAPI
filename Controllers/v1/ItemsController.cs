using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/items")]
    public class ItemsController : ApiController
    {
        private readonly IItemsService _itemService;
        public ItemsController(IItemsService itemsService)
        {
            _itemService = itemsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var itemDtos = await _itemService.GetAsync();
            return Ok(itemDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ItemDto), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var itemDto = await _itemService.GetAsync(id);
            return Ok(itemDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Post([FromBody] CreateUpdateItemDto createUpdateItemDto)
        {
            var createdItemId = await _itemService.CreateAsync(createUpdateItemDto);
            var createdResourceUrl = Url.Action(nameof(Get), new { id = createdItemId });
            return Created(createdResourceUrl!, null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _itemService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CreateUpdateItemDto createUpdateItemDto)
        {
            await _itemService.UpdateAsync(id, createUpdateItemDto);
            return NoContent();
        }
    }
}
