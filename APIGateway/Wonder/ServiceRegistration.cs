using Newtonsoft.Json;

namespace APIGateway.Wonder
{
    public class ServiceRegistration
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ServiceId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Host { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Port { get; set; }
    }
}
