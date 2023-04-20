using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.Authorization;
using KwiatkiBeatkiAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IAuthService
    {
        UserDto Login(AuthDto authDto);
        void SaveRefreshTokenData(AuthDto authDto, TokenDto tokenDto);
    }
    public class AuthorizeService : IAuthService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IPasswordHasher<UserEntity> _passwordHasher;
        private readonly IMapper _mapper;

        public AuthorizeService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IPasswordHasher<UserEntity> passwordHasher, IMapper mapper)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _passwordHasher = passwordHasher;
            _mapper = mapper;

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

        public void SaveRefreshTokenData(AuthDto authDto, TokenDto tokenDto)
        {
            var user = _kwiatkiBeatkiDbContext.User
                .FirstOrDefault(u => u.Email == authDto.Email);
            if (user == null)
                throw new BadRequestException("Invalid user name of password");

            user.RefreshToken = tokenDto.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(2);

            _kwiatkiBeatkiDbContext.SaveChanges();
        }
    }
}
