using IdentityModel;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Http.Headers;
using System.Text.Json;

namespace DeepIn.Identity.Server.Web.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddGitHub(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            var githubSection = configuration.GetSection("GitHub");
            if (!githubSection.Exists())
            {
                return builder;
            }
            // You must first create an app with GitHub and add its ID and Secret to your user-secrets.
            // https://github.com/settings/applications/
            // https://docs.github.com/en/developers/apps/authorizing-oauth-apps
            return builder.AddOAuth("GitHub", "GitHub", o =>
            {
                o.ClientId = githubSection["ClientId"];
                o.ClientSecret = githubSection["ClientSecret"];
                o.CallbackPath = githubSection["CallbackPath"] ?? "/signin-github";
                o.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                o.TokenEndpoint = "https://github.com/login/oauth/access_token";
                o.UserInformationEndpoint = "https://api.github.com/user";
                o.ClaimsIssuer = "OAuth2-Github";
                o.SaveTokens = true;
                // Retrieving user information is unique to each provider.
                o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                o.ClaimActions.MapJsonKey(ClaimTypes.Name, "login");
                o.ClaimActions.MapJsonKey("urn:github:name", "name");
                o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", ClaimValueTypes.Email);
                o.ClaimActions.MapJsonKey("urn:github:url", "url");
                o.ClaimActions.MapJsonKey(JwtClaimTypes.Picture, "avatar_url");
                o.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        // Get the GitHub user
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
                        response.EnsureSuccessStatusCode();

                        using (var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
                        {
                            context.RunClaimActions(user.RootElement);
                        }
                    }
                };
            });
        }
    }
}
