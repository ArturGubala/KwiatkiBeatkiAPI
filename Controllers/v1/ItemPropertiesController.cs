using KwiatkiBeatkiAPI.Models.ItemProperty;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/items/{itemId:int}/properties")]
    public class ItemPropertiesController : ApiController
    {
        private readonly IItemPropertiesService _itemPropertyiesService;
        public ItemPropertiesController(IItemPropertiesService itemPropertyiesService)
        {
            _itemPropertyiesService = itemPropertyiesService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Unprocessable entity")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Post([FromRoute] int itemId, [FromBody] CreateItemPropertyDto createItemPropertyDto)
        {
            var createdItemPropertyId = await _itemPropertyiesService.CreateAsync(itemId, createItemPropertyDto);
            return Created($"api/v1/items/{itemId}/properties/{createdItemPropertyId}", null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]

        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _itemPropertyiesService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "Forbidden")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateItemPropertyDto updateItemPropertyDto)
        {
            await _itemPropertyiesService.UpdateAsync(id, updateItemPropertyDto);
            return NoContent();
        }
    }
}
