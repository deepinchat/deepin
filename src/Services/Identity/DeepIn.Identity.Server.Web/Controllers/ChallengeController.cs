using DeepIn.Identity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.Server.Web.Controllers;
public class ChallengeController : Controller
{
    private readonly SignInManager<User> _signInManager;
    public ChallengeController(
        SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult ExternalLogin(string provider, string returnUrl)
    {
        var redirectUrl = new Uri(new Uri($"{Request.Scheme}://{Request.Host}"), $"/callback/external-login?returnUrl={returnUrl}");
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl.ToString());
        return Challenge(properties, provider);
    }
}