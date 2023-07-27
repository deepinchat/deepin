using DeepIn.Identity.Application.Services;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace DeepIn.Identity.Server.Web.Services
{
    public class CustomProfileService : DefaultProfileService
    {

        private readonly IUserService _userService;
        public CustomProfileService(ILogger<DefaultProfileService> logger,
            IUserService userService) : base(logger)
        {
            _userService = userService;
        }
        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.LogProfileRequest(Logger);
            var claims = context.Subject.Claims.ToList();
            if (context.Client.AllowedScopes.Any(s => s.Equals("profile")))
            {
                var userId = context.Subject.GetSubjectId();
                var profile = await _userService.GetProfileByUserId(userId);
                if (profile != null)
                {
                    var userClaims = profile.ToClaims(userId).Where(s => !string.IsNullOrEmpty(s.ClaimValue));
                    if (userClaims.Any())
                    {
                        claims = claims.Concat(userClaims.Select(s => s.ToClaim())).Distinct().ToList();
                    }
                }
            }
            context.AddRequestedClaims(claims);
            context.LogIssuedClaims(Logger);
            // return base.GetProfileDataAsync(context);
        }
    }
}
