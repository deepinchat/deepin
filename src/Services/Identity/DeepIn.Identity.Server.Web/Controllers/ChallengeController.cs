using DeepIn.Identity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;

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
        var urlBuilder = new UriBuilder($"{Request.Scheme}://{Request.Host}");
        urlBuilder.Path = "/callback/external-login";
        if (!string.IsNullOrEmpty(returnUrl))
        {
            urlBuilder.Query = $"returnUrl={Uri.EscapeDataString(returnUrl)}";
        }
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, urlBuilder.ToString());
        return Challenge(properties, provider);
    }
}