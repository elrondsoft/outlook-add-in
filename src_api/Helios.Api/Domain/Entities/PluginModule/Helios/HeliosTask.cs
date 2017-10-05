using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Helios
{
    public class HeliosTask
    {
        [JsonProperty("taskId")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("assignedTo")]
        public string AssignedTo { get; set; }
        [JsonProperty("dueDate")]
        public DateTime DueDate { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("priority")]
        public string Priority { get; set; }
        [JsonProperty("timeStamp")]
        public DateTime TimeStamp { get; set; }
        [JsonProperty("originatorId")]
        public string OriginatorId { get; set; }
        [JsonProperty("originatorId")]
        public DateTime LastModified { get; set; }
    }
}
