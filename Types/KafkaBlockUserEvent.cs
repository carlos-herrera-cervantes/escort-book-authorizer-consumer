using Newtonsoft.Json;

namespace EscortBookAuthorizerConsumer.Types
{
    public class KafkaBlockUserEvent
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}
