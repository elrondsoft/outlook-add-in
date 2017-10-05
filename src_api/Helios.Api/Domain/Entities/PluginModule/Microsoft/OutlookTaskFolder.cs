using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Microsoft
{
    public class OutlookTaskFolder
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
