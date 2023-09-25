using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [ProducesResponseType(typeof(UserDto), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Resource not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<IActionResult> Get()
        {
            UserDto userDto = await _usersService.GetSignInUserAsync();
            return Ok(userDto);
        }
    }
}
