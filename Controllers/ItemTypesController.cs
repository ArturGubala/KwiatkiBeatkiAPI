using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/item-types")]
    [ApiController]
    [Authorize]
    public class ItemTypesController : ControllerBase
    {
        private readonly IItemTypesService _itemTypeService;
        public ItemTypesController(IItemTypesService itemTypeService)
        {
            _itemTypeService = itemTypeService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _itemTypeService.GetAll();
            return Ok(items);
        }
    }
}
