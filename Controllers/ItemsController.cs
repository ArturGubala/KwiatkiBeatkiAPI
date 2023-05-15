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
        public async Task<IActionResult> Get()
        {
            var items = await _itemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _itemService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateItemDto createUpdateItemDto)
        {
            var createdItemId = await _itemService.CreateItemAsync(createUpdateItemDto);
            return Created($"api/items/{createdItemId}", null);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _itemService.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]CreateUpdateItemDto createUpdateItemDto) 
        {
            await _itemService.UpdateItemAsync(id, createUpdateItemDto);
            return NoContent();
        }
    }
}
