﻿using Newtonsoft.Json;

namespace Helios.Api.Domain.Dtos.Helios
{
    public class HeliosTokenResponceDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
