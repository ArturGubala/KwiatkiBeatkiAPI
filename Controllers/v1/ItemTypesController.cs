using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get()
        {
            var itemTypeDtos = await _itemTypeService.GetAsync();
            return Ok(itemTypeDtos);
        }
    }
}
