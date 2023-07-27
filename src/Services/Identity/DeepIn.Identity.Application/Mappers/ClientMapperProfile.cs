using AutoMapper;
using DeepIn.Identity.Application;
using DeepIn.Identity.Application.Models;
using IdentityServer4.EntityFramework.Entities;

namespace Identity.Application.Mappers
{
    public class ClientMapperProfile : Profile
    {
        public ClientMapperProfile()
        {
            CreateMap<ClientDto, Client>(MemberList.Source)
                .ForMember(d => d.Updated, o => o.MapFrom(s => DateTime.UtcNow))
                .ForMember(d => d.AllowedCorsOrigins, o => o.MapFrom(s => s.AllowedCorsOrigins.Select(x => new ClientCorsOrigin
                {
                    Origin = x
                })))
                .ForMember(d => d.AllowedGrantTypes, o => o.MapFrom(s => s.AllowedGrantTypes.Select(x => new ClientGrantType
                {
                    GrantType = x
                })))
                .ForMember(d => d.AllowedScopes, o => o.MapFrom(s => s.AllowedScopes.Select(x => new ClientScope
                {
                    Scope = x
                })))
                .ForMember(d => d.IdentityProviderRestrictions, o => o.MapFrom(s => s.IdentityProviderRestrictions.Select(x => new ClientIdPRestriction
                {
                    Provider = x
                })))
                .ForMember(d => d.PostLogoutRedirectUris, o => o.MapFrom(s => s.PostLogoutRedirectUris.Select(x => new ClientPostLogoutRedirectUri
                {
                    PostLogoutRedirectUri = x
                })))
                .ForMember(d => d.RedirectUris, o => o.MapFrom(s => s.RedirectUris.Select(x => new ClientRedirectUri
                {
                    RedirectUri = x
                })));

            CreateMap<Client, ClientModel>(MemberList.Destination)
                .ForMember(d => d.AllowedCorsOrigins, o => o.MapFrom(x => x.AllowedCorsOrigins.Select(x => x.Origin)))
                .ForMember(d => d.AllowedGrantTypes, o => o.MapFrom(x => x.AllowedGrantTypes.Select(x => x.GrantType)))
                .ForMember(d => d.AllowedScopes, o => o.MapFrom(x => x.AllowedScopes.Select(x => x.Scope)))
                .ForMember(d => d.IdentityProviderRestrictions, o => o.MapFrom(x => x.IdentityProviderRestrictions.Select(x => x.Provider)))
                .ForMember(d => d.PostLogoutRedirectUris, o => o.MapFrom(x => x.PostLogoutRedirectUris.Select(x => x.PostLogoutRedirectUri)))
                .ForMember(d => d.RedirectUris, o => o.MapFrom(x => x.RedirectUris.Select(x => x.RedirectUri)));

            CreateMap<SecretDto, ClientSecret>(MemberList.Source);
            CreateMap<PropertyDto, ClientProperty>(MemberList.Source);
            CreateMap<ClientClaimDto, ClientClaim>(MemberList.Source);

            CreateMap<ClientSecret, SecretModel>(MemberList.Destination);
            CreateMap<ClientProperty, PropertyModel>(MemberList.Destination);
            CreateMap<ClientClaim, ClientClaimModel>(MemberList.Destination);
        }
    }
}
