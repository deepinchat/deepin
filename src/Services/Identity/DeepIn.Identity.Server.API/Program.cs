using DeepIn.Identity.Application;
using DeepIn.Identity.Infrastructure;
using DeepIn.Service.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(typeof(Program).Assembly);

builder.Services.AddIdentityApplication()
                .AddIdentityDbContexts(
    builder.Configuration.GetConnectionString("IdentityConnection"),
    builder.Configuration.GetConnectionString("ConfigurationConnection"),
    builder.Configuration.GetConnectionString("PersistedGrantConnection"));

builder.Services.AddControllers();

var app = builder.Build();

app.UseServiceDefaults();

app.Run();
