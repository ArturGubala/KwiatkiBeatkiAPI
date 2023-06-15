using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : ApiController
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
