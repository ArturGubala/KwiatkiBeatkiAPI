using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IUsersService
    {
        Task<UserDto> GetSignInUserAsync();
    }
    public class UsersService : IUsersService
    {
        private readonly KwiatkiBeatkiDbContext _kwiatkiBeatkiDbContext;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public UsersService(KwiatkiBeatkiDbContext kwiatkiBeatkiDbContext, IMapper mapper, IUserContextService userContextService)
        {
            _kwiatkiBeatkiDbContext = kwiatkiBeatkiDbContext;
            _mapper = mapper;
            _userContextService = userContextService;
        }
        public async Task<UserDto> GetSignInUserAsync()
        {
            int loggedUserId = int.Parse(_userContextService.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            UserEntity? userEntity = await _kwiatkiBeatkiDbContext.User
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == loggedUserId);

            if (userEntity == null)
                throw new NotFoundException("UserId", $"User was not found");

            UserDto userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
        }
    }
}
