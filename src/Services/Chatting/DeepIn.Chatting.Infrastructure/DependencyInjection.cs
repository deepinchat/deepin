using DeepIn.Chatting.Domain.ChatAggregate;
using DeepIn.Chatting.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeepIn.Chatting.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddChattingInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly.GetName().Name;

        services.AddDbContext<ChattingDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
                sql.EnableRetryOnFailure(5);
            });
        });
        services.AddScoped<IChatRepository, ChatRepository>();
        return services;
    }
}

