using Newtonsoft.Json;

namespace Helios.Api.Domain.Dtos.Microsoft
{
    public class HeliosTokenRequestDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}