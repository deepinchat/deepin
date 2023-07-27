using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DeepIn.Caching.Redis;

public static class DependencyInjection
{
    public static IServiceCollection AddDeepInRedisCache(this IServiceCollection services, RedisCacheOptions options)
    {
        services.AddSingleton(sp =>
        {
            var configuration = ConfigurationOptions.Parse(options.ConnectionString, true);
            configuration.ResolveDns = true;
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddSingleton<ICacheManager>(s => new RedisCacheManager(s.GetRequiredService<ConnectionMultiplexer>(), options));
        return services;
    }
}
