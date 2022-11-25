using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EscortBookAuthorizer.Consumer.Models;

public class User : Base
{
    [BsonElement("verificationToken")]
    public string VerificationToken { get; set; }

    [BsonElement("verified")]
    public bool Verified { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("firebaseToken")]
    public string FirebaseToken { get; set; }

    [BsonElement("type")]
    public string Type { get; set; }

    [BsonElement("roles")]
    public List<string> Roles { get; set; }

    [BsonElement("block")]
    public bool Block { get; set; }

    [BsonElement("deactivated")]
    public bool Deactivated { get; set; }

    [BsonElement("delete")]
    public bool Delete { get; set; }
}
