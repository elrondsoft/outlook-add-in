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
        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }
        [JsonProperty("AuthorId")]
        public string AuthorId { get; set; }
    }

    public class HeliosTaskToUpdate
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; }
        [JsonProperty("AssignedTo")]
        public string AssignedTo { get; set; }
        [JsonProperty("Priority")]
        public string Priority { get; set; }
        [JsonProperty("OriginatorId")]
        public string OriginatorId { get; set; }
        [JsonProperty("Executor")]
        public string Executor { get; set; }
    }
}
