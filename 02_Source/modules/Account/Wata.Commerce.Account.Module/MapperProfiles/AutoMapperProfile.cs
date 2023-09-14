using AutoMapper;
using Wata.Commerce.Account.Dtos;
using Wata.Commerce.Account.Module.Models.Role;
//using Wata.Commerce.Account.Module.Models.Abc;

namespace Wata.Commerce.Account.Module.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RoleDto, RoleModel>();
            CreateMap<RoleModel, RoleRequestDto>();
        }
    }
}
