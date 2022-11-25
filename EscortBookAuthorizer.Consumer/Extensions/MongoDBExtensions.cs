using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace EscortBookAuthorizer.Consumer.Extensions;

public static class MongoDBExtensions
{
    public static IServiceCollection AddMongoDBClient(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_HOST");
        var client = new MongoClient(connectionString);

        services.AddSingleton<IMongoClient>(c => client);
        return services;
    }
}
