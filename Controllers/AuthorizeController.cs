using KwiatkiBeatkiAPI.Models.Authorization;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthorizeController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("/login")]
        public IActionResult Post([FromBody] AuthDto authDto)
        {
            var userDto = _authService.Login(authDto);
            var accessToken = _tokenService.GenerateAccessToken(userDto);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var tokenDto = new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
            _authService.SaveRefreshTokenData(authDto, tokenDto);

            return Ok(tokenDto);
        }
    }
}
