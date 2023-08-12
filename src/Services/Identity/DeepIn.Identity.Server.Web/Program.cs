using DeepIn.Identity.Infrastructure;
using DeepIn.Identity.Server.Web.Configurations;
using DeepIn.Identity.Server.Web.Extensions;
using DeepIn.Service.Common.Extensions;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults(typeof(Program).Assembly);

const string SERVICE_NAME = "Identity.Server";
var redisSection = builder.Configuration.GetSection("Caching");
if (redisSection.Exists())
{
    var redisConnection = redisSection["ConnectionString"];
    builder.Services.AddDataProtection(opts =>
    {
        opts.ApplicationDiscriminator = SERVICE_NAME;
    })
     .PersistKeysToStackExchangeRedis(ConnectionMultiplexer.Connect(redisConnection), $"{SERVICE_NAME}.DataProtection.Keys");
}

builder.Services.AddCustomIdenityServerAPI(builder.Configuration, builder.Environment);

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();
app
    .MigrateDbContext<IdentityObjectContext>((_, __) => { })
    .MigrateDbContext<ConfigurationObjectContext>((context, sp) =>
    {
        new ConfigurationDbContextSeed(app.Configuration)
            .SeedAsync(context).Wait();
    })
    .MigrateDbContext<PersistedGrantObjectContext>((_, __) => { });


app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
if (app.Environment.IsProduction())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 
app.Use(async (ctx, next) =>
{
    var identityServerSection = app.Configuration.GetSection("IdentityServer");
    ctx.SetIdentityServerOrigin(identityServerSection["Url"]);
    await next();
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseServiceDefaults();

app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    endpoints.MapHealthChecks("/health");
});
if (app.Environment.IsDevelopment())
{
    app.UseSpa(spa =>
    {
        // To learn more about options for serving an Angular SPA from ASP.NET Core,
        // see https://go.microsoft.com/fwlink/?linkid=864501

        spa.Options.SourcePath = "ClientApp";
        spa.UseProxyToSpaDevelopmentServer("http://localhost:4431");
    });
}
else
{
    app.MapFallbackToFile("index.html"); ;
}

app.Run();
