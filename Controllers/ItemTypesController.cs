﻿using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/item-types")]
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
        public async Task<IActionResult> Get()
        {
            var itemTypeDtos = await _itemTypeService.GetAsync();
            return Ok(itemTypeDtos);
        }
    }
}
