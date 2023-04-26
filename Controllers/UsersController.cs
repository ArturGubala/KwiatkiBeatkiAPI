using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IUserContextService _userContextService;
        public UsersController(IUsersService userService, IUserContextService userContextService)
        {
            _usersService = userService;
            _userContextService = userContextService;
        }
        [HttpGet("logged-user")]
        public IActionResult Get()
        {
            UserDto userDto = _usersService.GetLoggedUser();
            return Ok(userDto);
        }
    }
}
