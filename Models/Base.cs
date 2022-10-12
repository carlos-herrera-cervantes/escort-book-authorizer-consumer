using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EscortBookAuthorizerConsumer.Models;

public class Base
{
    [BsonElement("_id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("createdAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; set; }

    [BsonElement("updatedAt")]
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; set; }
}
