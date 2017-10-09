using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Helios
{
    public class HeliosTask
    {
        #region core properties
        [JsonProperty("Id")]
        public string Id
        {
            get => _entityId;
            set => _entityId = value;
        }
        [JsonProperty("taskId")]
        public string TaskId
        {
            get => _entityId;
            set => _entityId = value; // this stub is needed to work with helios fucking api
        }
        private string _entityId;
        [JsonProperty("title")]
        public string Subject { get; set; }
        [JsonProperty("description")]
        public string Body { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; } // New, Accepted, Completed, 
        [JsonProperty("priority")]
        public string Importance { get; set; } // Low, Normal, High
        [JsonProperty("dueDate")]
        public DateTime DueDateTime { get; set; }
        #endregion

        #region helios properties
        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }
        [JsonProperty("originatorId")]
        public string OriginatorId { get; set; }
        [JsonProperty("AuthorId")]
        public string AuthorId { get; set; }
        [JsonProperty("assignedTo")]
        public string AssignedTo { get; set; }
        [JsonProperty("Executor")]
        public string Executor { get; set; }
        #endregion
    }
}
