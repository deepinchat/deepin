using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeepIn.EventBus;

public static class DependencyInjection
{
    public static IServiceCollection AddDeepInEventBusInMemory(this IServiceCollection services, Assembly[] assemblies)
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddConsumers(assemblies);
            cfg.UsingInMemory((ctx, cfg) =>
            {
                cfg.ConfigureEndpoints(ctx);
            });
        });
        return services;
    }
}

