using KwiatkiBeatkiAPI.Models.Authorization;
using KwiatkiBeatkiAPI.Models.User;
using KwiatkiBeatkiAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KwiatkiBeatkiAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/authorize")]
    [ApiController]
    [AllowAnonymous]
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
        [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
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
        [ProducesResponseType(typeof(IEnumerable<TokenDto>), (int)StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad request")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
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
