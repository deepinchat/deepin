using DeepIn.Identity.Infrastructure;
using IdentityServer4.EntityFramework.Mappers;

namespace DeepIn.Identity.Server.Web.Configurations;

public class ConfigurationDbContextSeed
{
    private readonly IConfiguration _configuration;
    public ConfigurationDbContextSeed(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task SeedAsync(ConfigurationObjectContext context)
    {
        if (!context.IdentityResources.Any())
        {
            foreach (var resource in Config.GetResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }
            await context.SaveChangesAsync();
        }

        foreach (var scope in Config.GetApiScopes())
        {
            if (!context.ApiScopes.Any(x => x.Name == scope.Name))
            {

                context.ApiScopes.Add(scope.ToEntity());
                await context.SaveChangesAsync();
            }
        }


        foreach (var api in Config.GetApiResources())
        {
            if (!context.ApiResources.Any(x => x.Name == api.Name))
            {
                context.ApiResources.Add(api.ToEntity());
                await context.SaveChangesAsync();
            }
        }

        foreach (var client in Config.GetClients(_configuration))
        {
            if (!context.Clients.Any(x => x.ClientId == client.ClientId))
            {
                context.Clients.Add(client.ToEntity());
                await context.SaveChangesAsync();
            }
        }
    }
}
