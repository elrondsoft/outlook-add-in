using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Helios
{
    public class HeliosEvent
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("start")]
        public DateTime Start { get; set; }
        [JsonProperty("end")]
        public DateTime End { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("lastModifiedDateTime")]
        public DateTime LastModifiedDateTime { get; set; }

        public HeliosEvent() { }

        public HeliosEvent(string id, string title, string description, string category, DateTime start, DateTime end, DateTime lastModifiedDateTime)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            Start = start;
            End = end;
            LastModifiedDateTime = lastModifiedDateTime;
        }
    }
}
