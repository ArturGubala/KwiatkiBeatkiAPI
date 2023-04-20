using AutoMapper;
using KwiatkiBeatkiAPI.Entities.User;
using KwiatkiBeatkiAPI.Models.User;

namespace KwiatkiBeatkiAPI.MappingProfiles
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserEntity, UserDto>()
                .ForMember(m => m.RoleName, c => c.MapFrom(s => s.Role.Name));
        }
    }
}
