using AutoMapper;
using Wata.Commerce.Sample.Domain.Models;
using Wata.Commerce.Sample.Domain.Filters;
using Wata.Commerce.Sample.Dtos;

namespace Wata.Commerce.Sample.Business.MapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Abc, AbcDto>();
            CreateMap<AbcFilterDto, AbcFilter>();
        }
    }
}
