using KwiatkiBeatkiAPI.Models.Authorization;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/authorize")]
    [ApiController]
    public class AuthorizeController : ApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;

        public AuthorizeController(ITokenService tokenService, IAuthService authService)
        {
            _tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] AuthDto authDto)
        {
            var userDto = await _authService.LoginAsync(authDto);
            var accessToken = _tokenService.GenerateAccessToken(userDto);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var tokenDto = new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
            await _authService.SaveRefreshTokenDataAsync(userDto, tokenDto);

            return Ok(tokenDto);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Post([FromBody] TokenDto tokenDto)
        {
            var userDto = await _authService.CheckTokensAsync(tokenDto);
            var accessToken = _tokenService.GenerateAccessToken(userDto);
            var refreshToken = _tokenService.GenerateRefreshToken();
            var newTokenDto = new TokenDto()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
            await _authService.SaveRefreshTokenDataAsync(userDto, newTokenDto);

            return Ok(newTokenDto);
        }
    }
}
