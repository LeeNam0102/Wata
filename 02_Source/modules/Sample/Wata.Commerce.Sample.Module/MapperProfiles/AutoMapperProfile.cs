using AutoMapper;
using Wata.Commerce.Sample.Dtos;
using Wata.Commerce.Sample.Module.Models.Abc;

namespace Wata.Commerce.Sample.Module.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AbcDto, AbcModel>();
            CreateMap<AbcModel, AbcRequestDto>();
        }
    }
}
