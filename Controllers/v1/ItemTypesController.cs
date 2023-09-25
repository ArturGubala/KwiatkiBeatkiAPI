using KwiatkiBeatkiAPI.Models.ItemType;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/item-types")]
    public class ItemTypesController : ApiController
    {
        private readonly IItemTypesService _itemTypeService;
        public ItemTypesController(IItemTypesService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ItemTypeDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            var itemTypeDtos = await _itemTypeService.GetAsync();
            return Ok(itemTypeDtos);
        }
    }
}
