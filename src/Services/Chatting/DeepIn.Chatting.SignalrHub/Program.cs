using DeepIn.Service.Common.Extensions;
using DeepIn.Chatting.Application;
using DeepIn.Chatting.Infrastructure;
using StackExchange.Redis;
using Microsoft.AspNetCore.DataProtection;
using DeepIn.Chatting.SignalrHub.Hubs;

namespace DeepIn.Chatting.SignalrHub;
public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.AddServiceDefaults(eventHandlerAssembly: typeof(Program).Assembly);
        builder.Services.AddChattingInfrastructure(builder.Configuration);
        builder.Services.AddChattingApplication(builder.Configuration);

        const string SERVICE_NAME = "Chatting.SignalrHub";
        var redisSection = builder.Configuration.GetSection("Caching");
        if (redisSection.Exists())
        {
            var redisConnection = redisSection["ConnectionString"];
            builder.Services.AddDataProtection(opts =>
            {
                opts.ApplicationDiscriminator = SERVICE_NAME;
            })
             .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisConnection), $"{SERVICE_NAME}.DataProtection.Keys");

            builder.Services.AddSignalR().AddStackExchangeRedis(redisConnection, options => { });
        }
        else
        {
            builder.Services.AddSignalR();
        }

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseServiceDefaults();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ChatsHub>("/hubs/chats");
        });

        app.Run();
    }
}