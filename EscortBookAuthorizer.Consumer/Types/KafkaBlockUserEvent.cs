using Newtonsoft.Json;

namespace EscortBookAuthorizer.Consumer.Types;

public class KafkaBlockUserEvent
{
    [JsonProperty("userId")]
    public string UserId { get; set; }

    [JsonProperty("status")]
    public string Status { get; set; }
}
