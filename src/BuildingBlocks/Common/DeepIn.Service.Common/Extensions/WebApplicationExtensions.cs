using DeepIn.Service.Common.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeepIn.Service.Common.Extensions;

public static class WebApplicationExtensions
{
    public static IApplicationBuilder UseAllowAnyCorsPolicy(this IApplicationBuilder app)
    {
        app.UseCors(WebHostDefaults.AllowAnyCorsPolicy);
        return app;
    }
    public static WebApplication UseServiceDefaults(this WebApplication app)
    {

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        var pathBase = app.Configuration["PathBase"];

        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }
        app.UseRouting();
        app.UseAllowAnyCorsPolicy();
        var identitySection = app.Configuration.GetSection("Identity");

        if (identitySection.Exists())
        {
            // We have to add the auth middleware to the pipeline here
            app.UseAuthentication();
            app.UseAuthorization();
        }

        app.UseDefaultOpenApi(app.Configuration);

        app.MapDefaultHealthChecks();

        return app;
    }
    public static IApplicationBuilder UseDefaultOpenApi(this WebApplication app, IConfiguration configuration)
    {
        var openApiSection = configuration.GetSection("OpenApi");

        if (!openApiSection.Exists())
        {
            return app;
        }

        app.UseSwagger();
        app.UseSwaggerUI(setup =>
        {

            var pathBase = configuration["PathBase"];
            var authSection = openApiSection.GetSection("Auth");
            var endpointSection = openApiSection.GetRequiredSection("Endpoint");

            var swaggerUrl = endpointSection["Url"] ?? $"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json";

            setup.SwaggerEndpoint(swaggerUrl, endpointSection.GetRequiredValue("Name"));

            if (authSection.Exists())
            {
                setup.OAuthClientId(authSection.GetRequiredValue("ClientId"));
                setup.OAuthAppName(authSection.GetRequiredValue("AppName"));
            }
        });

        return app;
    }
    public static void MapDefaultHealthChecks(this IEndpointRouteBuilder routes)
    {
        routes.MapHealthChecks("/hc", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        routes.MapHealthChecks("/liveness", new HealthCheckOptions
        {
            Predicate = r => r.Name.Contains("self")
        });
    }
}
