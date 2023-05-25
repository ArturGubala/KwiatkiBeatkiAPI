using KwiatkiBeatkiAPI.Models.ItemProperty;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/items/{itemId:int}/properties")]
    [ApiController]
    [Authorize]
    public class ItemPropertiesController : ControllerBase
    {
        private readonly IItemPropertiesService _itemPropertyiesService;
        public ItemPropertiesController(IItemPropertiesService itemPropertyiesService)
        {
            _itemPropertyiesService = itemPropertyiesService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Post([FromRoute] int itemId, [FromBody] CreateItemPropertyDto createItemPropertyDto)
        {
            var createdItemPropertyId = _itemPropertyiesService.CreateItemProperty(itemId, createItemPropertyDto);
            return Created($"api/v1/items/{itemId}/properties/{createdItemPropertyId}", null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Delete([FromRoute] int id)
        {
            _itemPropertyiesService.DeleteItemProperty(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateItemPropertyDto updateItemPropertyDto)
        {
            _itemPropertyiesService.UpdateItemProperty(id, updateItemPropertyDto);
            return NoContent();
        }
    }
}
