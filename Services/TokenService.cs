using KwiatkiBeatkiAPI.Models;
using KwiatkiBeatkiAPI.Models.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace KwiatkiBeatkiAPI.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDto userDto);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
    public class TokenService : ITokenService
    {
        private readonly AutenticationSettings _autenticationSettings;
        public TokenService(AutenticationSettings autenticationSettings)
        {
            _autenticationSettings = autenticationSettings;
        }
        public string GenerateAccessToken(UserDto userDto)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{userDto.FirstName} {userDto.LastName}"),
                new Claim(ClaimTypes.Role, $"{userDto.RoleName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_autenticationSettings.JwtKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_autenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                    _autenticationSettings.JwtIssuer,
                    _autenticationSettings.JwtIssuer,
                    claims,
                    expires: expires,
                    signingCredentials: signingCredentials
                );

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_autenticationSettings.JwtKey)),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}
