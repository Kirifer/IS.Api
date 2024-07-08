using Is.Datalayer.Entities;
using Is.Models;

using AutoMapper;

namespace Is.Api.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AtsUserCreateDto, User>(MemberList.Destination);
            CreateMap<AtsUserUpdateDto, User>(MemberList.Destination);
            CreateMap<User, AtsUserDto>(MemberList.Destination);
        }
    }
}
