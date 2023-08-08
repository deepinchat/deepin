using DeepIn.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using static MassTransit.ValidationResultExtensions;

namespace DeepIn.Identity.Server.Web.Extensions;

public static class UserManagerExtensions
{
    public static async Task<User> CreateUserByExternalLoginAsync(this UserManager<User> userManager, ExternalLoginInfo loginInfo, ModelStateDictionary modelState = null)
    {
        var user = new User()
        {
            UserName = User.GenerateGuidUserName(),
            CreatedAt = DateTime.UtcNow,
            LastModifiedAt = DateTime.UtcNow
        };
        if (loginInfo.LoginProvider == AppDefaults.LoginProvider.GitHub)
        {
            user.Email = loginInfo.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var identityResult = await userManager.CreateAsync(user);
            if (identityResult.Succeeded)
            {
                await userManager.AddClaimsAsync(user, loginInfo.Principal.Claims);
                return user;
            }
            else if (modelState != null)
            {
                foreach (var error in identityResult.Errors)
                {
                    modelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return default;
    }
}
