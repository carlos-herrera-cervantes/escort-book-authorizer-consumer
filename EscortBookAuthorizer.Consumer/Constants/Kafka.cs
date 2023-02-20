using System;

namespace EscortBookAuthorizer.Consumer.Constants;

public static class KafkaTopic
{
    public const string BlockDeleteUser = "block-user";
}

public static class KafkaClient
{
    public static readonly string GroupId = Environment.GetEnvironmentVariable("KAFKA_GROUP_ID");

    public static readonly string Servers = Environment.GetEnvironmentVariable("KAFKA_SERVERS");
}
