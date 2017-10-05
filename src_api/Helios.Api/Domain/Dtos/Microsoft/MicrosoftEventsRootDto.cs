using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Dtos.Microsoft
{
    public class MicrosoftEventsRootDto
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }
        public IList<OutlookEvent> Value { get; set; }
    }
}
