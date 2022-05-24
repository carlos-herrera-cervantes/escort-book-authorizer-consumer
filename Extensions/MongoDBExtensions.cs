using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace EscortBookAuthorizerConsumer.Extensions
{
    public static class MongoDBExtensions
    {
        public static IServiceCollection AddMongoDBClient
        (
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            var connectionString = configuration["MongoDB:host"];
            var client = new MongoClient(connectionString);

            services.AddSingleton<MongoClient>(c => client);
            return services;
        }
    }
}
