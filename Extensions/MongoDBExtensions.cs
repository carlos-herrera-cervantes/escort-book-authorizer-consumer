using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;

namespace EscortBookAuthorizerConsumer.Extensions;

public static class MongoDBExtensions
{
    public static IServiceCollection AddMongoDBClient(this IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGO_DB_HOST");
        var client = new MongoClient(connectionString);

        services.AddSingleton<MongoClient>(c => client);
        return services;
    }
}
