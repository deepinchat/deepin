using DeepIn.Emailing.API.Application.IntegrationEvents.Events;
using DeepIn.Identity.Application.Models;
using DeepIn.Identity.Domain.Entities;
using DeepIn.Identity.Server.Web.Configurations;
using DeepIn.Identity.Server.Web.Extensions;
using DeepIn.Identity.Server.Web.Models.Account;
using DeepIn.Service.Common.Services;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeepIn.Identity.Server.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IEventService _events;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUserContext _userContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountController(
        ILogger<AccountController> logger,
        IEventService events,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IPublishEndpoint publishEndpoint,
        IUserContext userContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _events = events;
        _userManager = userManager;
        _signInManager = signInManager;
        _publishEndpoint = publishEndpoint;
        _userContext = userContext;
        _httpContextAccessor = httpContextAccessor; 
    }

    #region Login 
    [HttpPost]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var user = await GetUserByLogin(model.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberLogin, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.UserName));
                    return Ok();
                }
                else if (result.IsNotAllowed)
                {
                    return StatusCode(302, RoutersConfig.ConfirmEmail(user.Id, returnUrl));
                }
                else if (result.RequiresTwoFactor)
                {
                    return StatusCode(302, RoutersConfig.LoginWithTwoFactor(model.RememberLogin, returnUrl));
                }
                await _events.RaiseAsync(new UserLoginFailureEvent(user.UserName, AccountOptions.InvalidCredentials.Message));
            }
            ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentials.Key);
        }
        return BadRequest(ModelState);
    }

    [HttpGet]
    [Route("ExternalCallback")]
    [AllowAnonymous]
    public async Task<IActionResult> ExternalCallback()
    {
        var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        if (loginInfo == null)
        {
            ModelState.AddModelError(string.Empty, AccountOptions.InvalidExternalLoginErrorMessage);
        }
        else
        {
            var user = await _userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
            if (user == null)
            {
                user = new User()
                {
                    UserName = Domain.Entities.User.GenerateGuidUserName(),
                };
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    var addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                    if (!addLoginResult.Succeeded)
                    {
                        AddIdentityErrors(result);
                        await _userManager.DeleteAsync(user);
                    }
                }
                else
                    AddIdentityErrors(result);
            }
            if (ModelState.IsValid)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok();
            }
        }
        return BadRequest(ModelState);
    }
    #endregion
    #region Register
    [HttpPost]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        var existUser = await _userManager.FindByEmailAsync(model.Email);
        if (existUser == null)
        {
            var user = new User { Email = model.Email, UserName = model.Email, CreatedAt = DateTime.UtcNow };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await SendEmail(user, UserEmailType.Register);
                return Ok(new { Id = user.Id });
            }
            else
            {
                AddIdentityErrors(result);
            }
        }
        else
        {
            ModelState.AddModelError(nameof(model.Email), AccountOptions.EmailIsTakenErrorMessage);
        }
        return BadRequest(ModelState);
    }
    #endregion
    [HttpGet]
    [Route("ResendEmailConfirmation")]
    [AllowAnonymous]
    public async Task<IActionResult> ResendEmailConfirmation(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return BadRequest(ModelState);
        }
        await SendEmail(user, UserEmailType.VerificationCode);
        return Ok();

    }
    [HttpPost]
    [Route("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Ok();
        }
        await SendEmail(user, UserEmailType.ResetPassword);
        return Ok();
    }
    [HttpPost]
    [Route("ResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
        }
        return Ok();
    }
    [HttpPost]
    [Route("ConfirmEmail")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{model.UserId}'.");
        }
        var result = await _userManager.ConfirmEmailAsync(user, model.Code);
        if (!result.Succeeded)
        {
            AddIdentityErrors(result);
            return BadRequest(ModelState);
        }
        return Ok();
    }
    [HttpPost]
    [Route("LoginWith2fa")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWith2fa([FromBody] LoginWith2faModel model)
    {
        var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Unable to load two-factor authentication user.");
            return BadRequest(ModelState);
        }

        var authenticatorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

        var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, model.rememberLogin, model.RememberMachine);
        if (result.Succeeded)
        {
            return Ok();
        }
        else if (result.IsLockedOut)
        {
            return StatusCode(302, RoutersConfig.Lockout);
        }
        else
        {
            ModelState.AddModelError(string.Empty, AccountOptions.InvalidAuthenticatorCode.Key);
            return BadRequest(ModelState);
        }
    }
    [HttpPost]
    [Route("Logout")]
    public async Task<IActionResult> Logout()
    {
        if (User?.Identity.IsAuthenticated == true)
        {
            // delete local authentication cookie
            await _signInManager.SignOutAsync();

            // raise the logout event
            await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
        }
        return NoContent();
    }
    [HttpGet]
    [Route("CheckSession")]
    public IActionResult CheckSession()
    {
        return Ok();
    }
    #region helper APIs for the AccountController
    private async Task<User> GetUserByLogin(string login)
    {
        var user = await _userManager.FindByNameAsync(login);
        return user ?? await _userManager.FindByEmailAsync(login);
    }
    private void AddIdentityErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
    }
    private async Task SendEmail(User user, UserEmailType emailType)
    {
        var code = string.Empty;
        if (emailType == UserEmailType.Register)
        {
            code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        else if (emailType == UserEmailType.ResetPassword)
        {
            code = await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        _logger.LogInformation($"{user.Email},verify code is {code}");
        await _publishEndpoint.Publish(new SendEmailIntegrationEvent()
        {
            Body = emailType == UserEmailType.Register ? WelcomeEmailTemplate(user.UserName, code) : $"Your Verification Code is {code}",
            IpAddress = _httpContextAccessor.GetIpAddress(),
            IsBodyHtml = true,
            Subject = emailType == UserEmailType.Register ? "Confirm your email address" : "Verify your email address",
            To = user.Email
        });

    }
    #endregion
    private static string WelcomeEmailTemplate(string userName, string code) => $@"
<p>Hello {userName},</p>
<br/>
<p>
Thanks for registering at our website. To help protect your privacy and ensure security, we require you to complete a verification process.
</p>
<br/>
<p
Please enter the verification code <h3>{code}</h3> on our website to complete your registration. If you have any concerns or did not initiate this registration, please contact us immediately.
</p>
<br/>
<p>
Thank you for choosing our website. We look forward to having you as a member of our community.
</p>
<br/>
Best regards,
Lance Chat
";
}
