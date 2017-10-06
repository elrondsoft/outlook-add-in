using Newtonsoft.Json;

namespace Helios.Api.Domain.Dtos.Helios.Api
{
    public class HeliosUserDetailsResponceDto
    {
        [JsonProperty("EntityId")]
        public string UserEntityId { get; set; }
        [JsonProperty("ApiKey")]
        public string ApiKey { get; set; }
    }
}
