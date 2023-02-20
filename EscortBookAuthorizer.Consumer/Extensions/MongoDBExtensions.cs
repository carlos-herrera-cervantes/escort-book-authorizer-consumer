using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using EscortBookAuthorizer.Consumer.Constants;

namespace EscortBookAuthorizer.Consumer.Extensions;

public static class MongoDBExtensions
{
    public static IServiceCollection AddMongoDBClient(this IServiceCollection services)
    {
        var client = new MongoDB.Driver.MongoClient(MongoClientConfig.ConnectionString);
        services.AddSingleton<IMongoClient>(c => client);
        return services;
    }
}
