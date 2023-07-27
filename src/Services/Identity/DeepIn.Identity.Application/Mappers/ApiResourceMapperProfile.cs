using AutoMapper;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace DeepIn.Identity.Application.Mappers
{
    public class ApiResourceMapperProfile : Profile
    {
        public ApiResourceMapperProfile()
        {
            CreateMap<ApiResourceDto, ApiResource>(MemberList.Source)
                .ForMember(d => d.Updated, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.Scopes, o => o.MapFrom(s => s.Scopes.Select(x => new ApiResourceScope
                {
                    Scope = x
                })))
                .ForMember(d => d.UserClaims, o => o.MapFrom(s => s.UserClaims.Select(x => new ApiResourceClaim
                {
                    Type = x
                })));

            CreateMap<ApiResource, ApiResourceModel>(MemberList.Destination)
                .ForMember(d => d.Scopes, o => o.MapFrom(s => s.Scopes.Select(x => x.Scope)))
                .ForMember(d => d.UserClaims, o => o.MapFrom(s => s.UserClaims.Select(x => x.Type)));

            CreateMap<SecretDto, ApiResourceSecret>(MemberList.Source);
            CreateMap<PropertyDto, ApiResourceProperty>(MemberList.Source);

            CreateMap<ApiResourceSecret, SecretModel>(MemberList.Destination);
            CreateMap<ApiResourceProperty, PropertyModel>(MemberList.Destination);
        }
    }
}
