using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Dtos.Microsoft.Api.Tasks
{
    public class MicrosoftTasksRootDto
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }
        [JsonProperty("value")]
        public IList<OutlookTask> Value { get; set; }
    }
}
