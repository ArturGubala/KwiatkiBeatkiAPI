using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/items")]
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

        [HttpPost]
        public IActionResult Post([FromBody] CreateUpdateItemDto createUpdateItemDto)
        {
            var createdItemId = _itemService.CreateItem(createUpdateItemDto);
            return Created($"api/items/{createdItemId}", null);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute]int id, [FromBody]CreateUpdateItemDto createUpdateItemDto) 
        {
            _itemService.UpdateItem(id, createUpdateItemDto);
            return NoContent();
        }
    }
}
