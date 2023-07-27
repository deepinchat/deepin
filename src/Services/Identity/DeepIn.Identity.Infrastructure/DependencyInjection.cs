using IdentityServer4.EntityFramework.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DeepIn.Identity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddIdentityDbContexts(this IServiceCollection services,
         string identityConnectionString,
         string configurationConnectionString,
         string persistedGrantConnectionString)
    {
        var migrationsAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly.GetName().Name;

        services.AddDbContext<IdentityObjectContext>(options =>
        {
            options.UseNpgsql(identityConnectionString, sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
                sql.EnableRetryOnFailure(5);
            });
        });
        services.AddConfigurationDbContext<ConfigurationObjectContext>(options =>
            options.ConfigureDbContext = ob =>
            {
                ob.UseNpgsql(configurationConnectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    sql.EnableRetryOnFailure(5);
                });
            });

        services.AddOperationalDbContext<PersistedGrantObjectContext>(options =>
            options.ConfigureDbContext = ob =>
            {
                ob.UseNpgsql(persistedGrantConnectionString, sql =>
                {
                    sql.MigrationsAssembly(migrationsAssembly);
                    sql.EnableRetryOnFailure(5);
                });
            });
        return services;
    }
}
