using DeepIn.Emailing.API.Application.Services;
using DeepIn.Emailing.API.Configurations;
using DeepIn.Emailing.API.Infrastructure;
using DeepIn.Service.Common.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DeepIn.Emailing.API;

internal static class DependencyInjection
{
    public static IServiceCollection AddEmailingServices(this IServiceCollection services, IConfiguration configuration)
    {
        var migrationsAssembly = typeof(DependencyInjection).GetTypeInfo().Assembly.GetName().Name;

        services.AddDbContext<EmailingDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetRequiredConnectionString("DefaultConnection"), sql =>
            {
                sql.MigrationsAssembly(migrationsAssembly);
                sql.EnableRetryOnFailure(5);
            });
        });

        var smtpSection = configuration.GetSection("Smtp");
        services.AddSingleton<IEmailSender>(sp => new SmtpEmailSender(sp.GetService<ILogger<SmtpEmailSender>>(), smtpSection.Get<SmtpOtions>()));
        return services;
    }
}
