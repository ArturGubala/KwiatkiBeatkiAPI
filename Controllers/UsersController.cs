﻿using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService userService)
        {
            _usersService = userService;
        }
        [HttpGet("sign-in")]
        public async Task<IActionResult> Get()
        {
            UserDto userDto = await _usersService.GetSignInUserAsync();
            return Ok(userDto);
        }
    }
}
