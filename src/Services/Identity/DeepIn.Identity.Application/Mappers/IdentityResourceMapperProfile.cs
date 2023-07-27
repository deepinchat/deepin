using AutoMapper;
using DeepIn.Identity.Application;
using IdentityServer4.EntityFramework.Entities;

namespace Identity.Application.Mappers
{
    public class IdentityResourceMapperProfile : Profile
    {
        public IdentityResourceMapperProfile()
        {
            CreateMap<IdentityResourceDto, IdentityResource>(MemberList.Source)
                .ForMember(d => d.Updated, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.UserClaims, o => o.MapFrom((s, d) => s.UserClaims.Select(x => new ApiResourceClaim
                {
                    Type = x
                })));

            CreateMap<PropertyDto, IdentityResourceProperty>(MemberList.Source);
        }
    }
}
