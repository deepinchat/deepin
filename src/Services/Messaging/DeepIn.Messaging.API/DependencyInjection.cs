using DeepIn.Messaging.API.Domain;
using DeepIn.Messaging.API.Infrastructure.Repositories;
using DeepIn.Messaging.API.Services;
using MongoDB.Driver;

namespace DeepIn.Messaging.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(s => new MongoClient(configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton(s => new MessagingDbContext(s.GetRequiredService<MongoClient>(), configuration["DbName"]));
            services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddScoped<IMessageService, MessageService>();
            return services;
        }
    }
}
