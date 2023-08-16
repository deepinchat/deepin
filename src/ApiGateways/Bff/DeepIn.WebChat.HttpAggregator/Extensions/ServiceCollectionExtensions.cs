using DeepIn.Service.Common;
using DeepIn.WebChat.HttpAggregator.HttpClients;
using DeepIn.WebChat.HttpAggregator.Services;

namespace DeepIn.WebChat.HttpAggregator.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register delegating handlers
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        // Register http services
        services.AddHttpClient<IMessageHttpClient, MessageHttpClient>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();


        services.AddHttpClient<IUserHttpClient, UserProfileHttpClient>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddScoped<IMessageService, MessageService>();
        return services;
    }
}
