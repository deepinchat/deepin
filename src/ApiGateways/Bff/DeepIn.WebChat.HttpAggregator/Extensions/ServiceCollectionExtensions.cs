using DeepIn.Service.Common;
using DeepIn.WebChat.HttpAggregator.Config;
using DeepIn.WebChat.HttpAggregator.HttpClients;
using DeepIn.WebChat.HttpAggregator.Services;

namespace DeepIn.WebChat.HttpAggregator.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<UrlsConfig>(configuration.GetSection("Urls"));
        // Register delegating handlers
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        // Register http services
        services.AddHttpClient<IMessageHttpClient, MessageHttpClient>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


        services.AddHttpClient<IUserProfileHttpClient, UserProfileHttpClient>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddScoped<IMessageService, MessageService>();
        return services;
    }
}
