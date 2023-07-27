using DeepIn.Identity.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeepIn.Identity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
        });
        services.AddAutoMapper(assembly);

        services.AddScoped<IApiResourceService, ApiResourceService>();
        services.AddScoped<IApiScopeService, ApiScopeService>();
        services.AddScoped<IIdentityResourceService, IdentityResourceService>();
        services.AddScoped<IClientService, ClientService>();

        services.AddScoped<IUserService, UserService>();
        return services;
    }
}
