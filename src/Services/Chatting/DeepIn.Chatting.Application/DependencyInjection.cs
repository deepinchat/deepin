using DeepIn.Caching;
using DeepIn.Chatting.Application.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeepIn.Chatting.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddChattingApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IChatQueries>(sp => new ChatQueries(configuration.GetConnectionString("DefaultConnection"), sp.GetRequiredService<ICacheManager>()));

        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(assembly);
        });
        return services;
    }
}
