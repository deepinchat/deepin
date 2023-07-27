using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeepIn.EventBus.RabbitMQ
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDeepInEventBusRabbitMQ(this IServiceCollection services, RabbitMQOptions mqConfig, params Assembly[] assemblies)
        {
            services.AddMassTransit(config =>
            {
                config.AddConsumers(assemblies);
                config.UsingRabbitMq((context, mq) =>
                {
                    mq.Host(mqConfig.HostName, mqConfig.VirtualHost, h =>
                    {
                        h.Username(mqConfig.Username);
                        h.Password(mqConfig.Password);

                    });
                    mq.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
