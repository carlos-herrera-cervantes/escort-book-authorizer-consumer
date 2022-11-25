using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EscortBookAuthorizer.Consumer.Models;

public class AccessToken : Base
{
    [BsonElement("user")]
    public string User { get; set; }

    [BsonElement("token")]
    public string Token { get; set; }
}
