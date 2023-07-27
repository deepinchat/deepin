using AutoMapper;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace Identity.Application.Mappers
{
    public class ApiScopeMapperProfile : Profile
    {
        public ApiScopeMapperProfile()
        {
            CreateMap<ApiScopeDto, ApiScope>(MemberList.Source)
                .ForMember(d => d.UserClaims, o => o.MapFrom(s => s.UserClaims.Select(x => new ApiResourceClaim
                {
                    Type = x
                })));

            CreateMap<ApiScope, ApiScopeModel>(MemberList.Destination)
                .ForMember(d => d.UserClaims, o => o.MapFrom(s => s.UserClaims.Select(x => x.Type)));

            CreateMap<PropertyDto, ApiScopeProperty>(MemberList.Source);
            CreateMap<ApiScopeProperty, PropertyModel>(MemberList.Destination);
        }
    }
}
