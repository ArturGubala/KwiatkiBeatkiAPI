using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Authorization;
using KwiatkiBeatkiAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IAuthService
    {
        UserDto Login(AuthDto authDto);
        void SaveRefreshTokenData(UserDto userDto, TokenDto tokenDto);
        UserDto CheckTokens(TokenDto tokenDto);
    }
    public class AuthorizeService : IAuthService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthorizeService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IPasswordHasher<UserEntity> passwordHasher, 
                                IMapper mapper, ITokenService tokenService)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public UserDto Login(AuthDto authDto)
        {
            var user = _kwiatkiBeatkiDbContext.User
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == authDto.Email);
            if (user == null)
                throw new BadRequestException("Invalid username or password");

            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, authDto.Password);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public UserDto CheckTokens(TokenDto tokenDto)
        {
            string? accessToken = tokenDto.AccessToken;
            string? refreshToken = tokenDto.RefreshToken;

            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);

            var userId = int.Parse(principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var user = _kwiatkiBeatkiDbContext.User
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == userId);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new BadRequestException("Invalid client request");

            UserDto userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public void SaveRefreshTokenData(UserDto userDto, TokenDto tokenDto)
        {
            var user = _kwiatkiBeatkiDbContext.User.SingleOrDefault(u => u.Id == userDto.Id);

            if (user == null)
                throw new BadRequestException("Invalid client request");

            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            _kwiatkiBeatkiDbContext.SaveChanges();
        }


    }
}
