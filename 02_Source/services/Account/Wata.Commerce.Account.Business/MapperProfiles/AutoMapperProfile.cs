using AutoMapper;
using Wata.Commerce.Account.Domain.Models;
using Wata.Commerce.Account.Domain.Filters;
using Wata.Commerce.Account.Dtos;

namespace Wata.Commerce.Account.Business.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleFilterDto, RoleFilter>();
            CreateMap<User, UserDto>();
            CreateMap<UserFilterDto, UserFilter>();
        }
    }
}
