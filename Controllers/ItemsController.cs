using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemService;
        public ItemsController(IItemsService itemsService)
        {
            _itemService = itemsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var items = _itemService.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var item = _itemService.GetById(id);
            return Ok(item);
        }
    }
}
