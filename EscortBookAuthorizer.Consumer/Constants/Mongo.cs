using System;

namespace EscortBookAuthorizer.Consumer.Constants;

public static class MongoClientConfig
{
  public static readonly string ConnectionString = Environment.GetEnvironmentVariable("MONGO_DB_HOST");
}

public static class MongoDatabase
{
    public static readonly string Authorizer = Environment.GetEnvironmentVariable("MONGODB_DEFAULT_DB");
}
