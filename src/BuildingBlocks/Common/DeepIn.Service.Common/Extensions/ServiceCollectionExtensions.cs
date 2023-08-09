using DeepIn.Caching;
using DeepIn.Caching.Redis;
using DeepIn.EventBus.RabbitMQ;
using DeepIn.Service.Common.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;

namespace DeepIn.Service.Common.Extensions;
public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder, Assembly eventHandlerAssembly = null)
    {
        builder.Services.AddAllowAnyCorsPolicy();
        // Default health checks assume the event bus and self health checks
        builder.Services.AddDefaultHealthChecks(builder.Configuration);

        //Add the distributed cache
        builder.Services.AddCaching(builder.Configuration);

        // Add the event bus
        builder.Services.AddEventBus(builder.Configuration, eventHandlerAssembly);

        builder.Services.AddDefaultAuthentication(builder.Configuration);

        builder.Services.AddDefaultOpenApi(builder.Configuration);

        // Add the accessor
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddTransient<IUserContext, UserContext>();

        return builder;
    }

    public static IHealthChecksBuilder AddDefaultHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var hcBuilder = services.AddHealthChecks();

        // Health check for the application itself
        hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus | RabbitMQ",
        //   }
        // }

        var eventBusSection = configuration.GetSection("EventBus");

        if (!eventBusSection.Exists())
        {
            return hcBuilder;
        }

        return eventBusSection["ProviderName"]?.ToLowerInvariant() switch
        {
            "servicebus" => hcBuilder.AddAzureServiceBusTopic(
                    _ => configuration.GetRequiredConnectionString("EventBus"),
                    _ => "deepin_event_bus",
                    name: "servicebus",
                    tags: new string[] { "ready" }),

            _ => hcBuilder.AddRabbitMQ(
                    _ => $"amqp://{configuration.GetRequiredConnectionString("EventBus")}",
                    name: "rabbitmq",
                    tags: new string[] { "ready" })
        };
    }
    public static IServiceCollection AddDefaultOpenApi(this IServiceCollection services, IConfiguration configuration)
    {
        var openApi = configuration.GetSection("OpenApi");

        if (!openApi.Exists())
        {
            return services;
        }

        services.AddEndpointsApiExplorer();

        return services.AddSwaggerGen(options =>
        {
            /// {
            ///   "OpenApi": {
            ///     "Document": {
            ///         "Title": ..
            ///         "Version": ..
            ///         "Description": ..
            ///     }
            ///   }
            /// }
            var document = openApi.GetRequiredSection("Document");

            var version = document.GetRequiredValue("Version") ?? "v1";

            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = document.GetRequiredValue("Title"),
                Version = version,
                Description = document.GetRequiredValue("Description")
            });

            var identitySection = configuration.GetSection("Identity");

            if (!identitySection.Exists())
            {
                // No identity section, so no authentication open api definition
                return;
            }

            // {
            //   "Identity": {
            //     "ExternalUrl": "http://identity",
            //     "Scopes": {
            //         "basket": "Basket API"
            //      }
            //    }
            // }

            var identityUrlExternal = identitySection["ExternalUrl"] ?? identitySection.GetRequiredValue("Url");
            var scopes = identitySection.GetRequiredSection("Scopes").GetChildren().ToDictionary(p => p.Key, p => p.Value);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    Implicit = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{identityUrlExternal}/connect/authorize"),
                        TokenUrl = new Uri($"{identityUrlExternal}/connect/token"),
                        Scopes = scopes,
                    }
                }
            });

            // options.OperationFilter<AuthorizeCheckOperationFilter>();
        });
    }
    public static IServiceCollection AddDefaultAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "Identity": {
        //     "Url": "http://identity",
        //     "Audience": "basket"
        //    }
        // }

        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }

        // prevent from mapping "sub" claim to nameidentifier.
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
        {
            var identityUrl = identitySection.GetRequiredValue("Url");
            var audience = identitySection.GetRequiredValue("Audience");
            var allowTokenFromUrl = identitySection.GetValue<bool>("AllowUrlToken");//?.Equals(bool.TrueString, StringComparison.CurrentCultureIgnoreCase);

            options.Authority = identityUrl;
            options.RequireHttpsMetadata = false;
            options.Audience = audience;
            options.TokenValidationParameters.ValidateAudience = false;
            if (allowTokenFromUrl) 
            {
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        if (string.IsNullOrEmpty(context.Token))
                        {
                            var path = context.HttpContext.Request.Path;
                            if (path.StartsWithSegments("/hub"))
                            {
                                var accessToken = context.Request.Query["access_token"];
                                if (!string.IsNullOrEmpty(accessToken))
                                {
                                    context.Token = accessToken;
                                }
                            }
                        }
                        return Task.CompletedTask;
                    }
                };
            }
        });

        return services;
    }
    public static IServiceCollection AddAllowAnyCorsPolicy(this IServiceCollection services)
    {
        return services.AddCors(options =>
        {
            options.AddPolicy(WebHostDefaults.AllowAnyCorsPolicy,
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration, Assembly assembly = null)
    {
        // {
        //   "EventBus": {
        //     "ProviderName": "RabbitMQ",
        //     "QueueName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }

        var eventBusSection = configuration.GetSection("EventBus");
        if (!eventBusSection.Exists())
        {
            return services;
        }
        if (string.Equals(eventBusSection["ProviderName"], RabbitMQOptions.ProviderKey, StringComparison.OrdinalIgnoreCase))
        {
            var mqConfig = eventBusSection.Get<RabbitMQOptions>();
            services.AddDeepInEventBusRabbitMQ(mqConfig, assembly);
        }
        return services;
    }
    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        // {
        //   "Caching": {
        //     "ProviderName": "InMemory | Redis",
        //     "ConnectionString": "...",
        //     "DefaultCacheTimeMinutes": 30,
        //     "Database": 0,
        //   }
        // }

        var cacheSection = configuration.GetSection("Caching");
        if (!cacheSection.Exists())
        {
            return services;
        }
        if (string.Equals(cacheSection["ProviderName"], RedisCacheOptions.ProviderKey, StringComparison.OrdinalIgnoreCase))
        {
            var redisConfig = cacheSection.Get<RedisCacheOptions>();
            services.AddDeepInRedisCache(redisConfig);
        }
        else
        {
            var inMemoryOptions = cacheSection.Get<CacheOptions>();
            services.AddDeepInMemoryCache(inMemoryOptions);
        }
        return services;
    }

}
