﻿using KwiatkiBeatkiAPI.Models.Item;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/items")]
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
            var itemDtos = await _itemService.GetAllAsync();
            return Ok(itemDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var itemDto = await _itemService.GetByIdAsync(id);
            return Ok(itemDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Post([FromBody] CreateUpdateItemDto createUpdateItemDto)
        {
            var createdItemId = await _itemService.CreateItemAsync(createUpdateItemDto);
            return Created($"api/v1/items/{createdItemId}", null);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _itemService.DeleteItemAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Menager")]
        public async Task<IActionResult> Put([FromRoute]int id, [FromBody]CreateUpdateItemDto createUpdateItemDto) 
        {
            await _itemService.UpdateItemAsync(id, createUpdateItemDto);
            return NoContent();
        }
    }
}
