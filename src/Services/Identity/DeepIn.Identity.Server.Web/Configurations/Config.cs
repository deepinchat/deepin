using IdentityServer4;
using IdentityServer4.Models;

namespace DeepIn.Identity.Server.Web.Configurations
{
    internal class Config
    {
        private static string[] StandardScopes => new string[]
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            };
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("bss", "Blob Storage Service"),
                new ApiScope("identity", "Identity Service"),
                new ApiScope("identity.admin", "Identity Service Admin API"),
                new ApiScope("chat", "Chat Service"),
                new ApiScope("message", "Message Service"),
                new ApiScope("bff.web.chat", "Web chat bff service"),
            };
        }
        // ApiResources define the apis in your system
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("bss.api", "Blob Storage Service")
                {
                    Scopes = new string[]{ "bss" }
                },
                new ApiResource("identity.api", "Identity Service")
                {
                    Scopes = new string[]{ "identity", "identity.admin" }
                },
                new ApiResource("chat.api", "Chat Service")
                {
                    Scopes = new string[]{ "chat" }
                },
                new ApiResource("message.api", "Message Service")
                {
                    Scopes = new string[]{ "message" }
                },
                new ApiResource("bff.web.chat", "Web chat bff service")
                {
                    Scopes = new string[]{ "bff.web.chat" }
                },
            };
        }

        // Identity resources are data like user ID, name, or email address of a user
        // see: http://docs.identityserver.io/en/release/configuration/resources.html
        public static IEnumerable<IdentityResource> GetResources()
        {

            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }
        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "web_chat_client",
                    ClientName = "Web Chat Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = {
                        $"{"http://localhost:4200"}/callback/signin",
                        $"{"http://localhost:4200"}/callback/silent",
                        $"{"https://deepin.chat"}/callback/signin",
                        $"{"https://deepin.chat"}/callback/silent"
                    },
                    PostLogoutRedirectUris = {
                        $"{"http://localhost:4200"}/callback/logout",
                        $"{"https://deepin.chat"}/callback/logout",
                    },
                    AllowedCorsOrigins = {
                        "http://localhost:4200",
                        "https://deepin.chat"
                    },
                    AllowedScopes = StandardScopes.Concat(new string[]{
                    "bss",
                    "identity",
                    "chat",
                    "message",
                    "bff.web.chat"
                    }).ToArray(),
                },
                new Client
                {
                    ClientId="system_bot_client",
                    ClientName = "System Bot Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets ={ new Secret("7292716c64f04929ae6a4e2c70ed868d".Sha256()) },
                    AllowedScopes = StandardScopes.Concat( new string[]{
                    "bss",
                    "identity",
                    "chat",
                    "message"
                    }).ToArray(),
                },
                new Client
                {
                    ClientId = "messageswaggerui",
                    ClientName = "Message Swagger UI",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{configuration["MessageApiClient"]}/swagger/oauth2-redirect.html" },
                    PostLogoutRedirectUris = { $"{configuration["MessageApiClient"]}/swagger/" },

                    AllowedScopes =
                    {
                        "message"
                    }
                },
            };
        }
    }
}
