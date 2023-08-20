using DeepIn.Identity.API;
using DeepIn.Service.Common.Extensions;
using DeepIn.Identity.Infrastructure;
using DeepIn.Identity.Application;
namespace DeepIn.Identity.API;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.AddServiceDefaults(typeof(Program).Assembly);

        builder.Services.AddIdentityApplication()
                        .AddIdentityDbContexts(
            builder.Configuration.GetConnectionString("IdentityConnection"),
            builder.Configuration.GetConnectionString("ConfigurationConnection"),
            builder.Configuration.GetConnectionString("PersistedGrantConnection"));


        var app = builder.Build();

        app.UseServiceDefaults();


        app.MapControllers();

        app.Run();
    }
}