using Microsoft.EntityFrameworkCore;
using DeepIn.Identity.Application;
using DeepIn.Identity.Infrastructure;
using DeepIn.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net;
using DeepIn.Identity.Server.Web.Services;
using System.Security.Cryptography.X509Certificates;
using DeepIn.Service.Common.Services;

namespace DeepIn.Identity.Server.Web.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomIdenityServerAPI(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        services
            .AddIdentityApplication()
            .AddIdentityDbContexts(configuration.GetConnectionString("IdentityConnection"), configuration.GetConnectionString("ConfigurationConnection"), configuration.GetConnectionString("PersistedGrantConnection"));

        services.AddIdentityApplication()
                        .AddIdentityDbContexts(
            configuration.GetConnectionString("IdentityConnection"),
            configuration.GetConnectionString("ConfigurationConnection"),
            configuration.GetConnectionString("PersistedGrantConnection"));
        services.AddCustomIdentity();
        services.AddCustomIdentityServer(configuration, environment);
        return services;
    }
    public static IServiceCollection AddCustomIdentityServer(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var identityBuilder = services.AddIdentityServer(options =>
        {
            options.Events.RaiseErrorEvents = true;
            options.Events.RaiseInformationEvents = true;
            options.Events.RaiseFailureEvents = true;
            options.Events.RaiseSuccessEvents = true;
        })
         // this adds the config data from DB (clients, resources)
         .AddConfigurationStore<ConfigurationObjectContext>()
         // this adds the operational data from DB (codes, tokens, consents)
         .AddOperationalStore<PersistedGrantObjectContext>(options =>
         {
             // this enables automatic token cleanup. this is optional.
             options.EnableTokenCleanup = true;
         })
         .AddAspNetIdentity<User>()
         .AddProfileService<CustomProfileService>();

        if (environment.IsDevelopment())
        {
            identityBuilder.AddDeveloperSigningCredential();
        }
        else
        {
            var identityServerSection = configuration.GetSection("IdentityServer");
            identityBuilder
           .AddSigningCredential(new X509Certificate2(identityServerSection.GetRequiredValue("CertificatePath"), identityServerSection.GetRequiredValue("CertificatePassword")));
        }
        //services.AddTransient<IProfileService, CustomProfileService>();
        services.AddAuthentication()
            .AddGitHub(configuration);
        return services;
    }
    public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            options.Password = new PasswordOptions
            {
                RequireDigit = true,
                RequireLowercase = false,
                RequireUppercase = false,
                RequiredUniqueChars = 0,
                RequireNonAlphanumeric = false,
                RequiredLength = 8
            };
            options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
            options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<IdentityObjectContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(o =>
        {
            o.Cookie.Name = "deepin.identity";
            o.Events.OnRedirectToLogin = ctx =>
            {
                if (ctx.Request.Path.StartsWithSegments("/api"))
                {
                    ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                else
                {
                    ctx.Response.Redirect(ctx.RedirectUri);
                }
                return Task.FromResult(0);
            };
            o.LoginPath = new PathString("/signin");
            o.LogoutPath = new PathString("/signout");
        });
        return services;
    }
}
