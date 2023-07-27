using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace DeepIn.Caching;
public static class DependencyInjection
{
    public static IServiceCollection AddDeepInMemoryCache(this IServiceCollection services, CacheOptions options)
    {
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager>(s => new MemoryCacheManager(s.GetRequiredService<IMemoryCache>(), options));
        return services;
    }
}
