using AutoMapper;
using KwiatkiBeatkiAPI.DatabaseContext;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Exeptions;
using KwiatkiBeatkiAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace KwiatkiBeatkiAPI.Services
{
    public interface IUsersService
    {
        UserDto GetLoggedUser();
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
        public UserDto GetLoggedUser()
        {
            int loggedUserId = int.Parse(_userContextService.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            UserEntity? userEntity = _kwiatkiBeatkiDbContext.User
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == loggedUserId);

            if (userEntity == null)
                throw new NotFoundException("User not found");

            UserDto userDto = _mapper.Map<UserDto>(userEntity);

            return userDto;
            
        }
    }
}
