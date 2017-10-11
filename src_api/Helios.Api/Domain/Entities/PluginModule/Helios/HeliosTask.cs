using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Helios
{
    public class HeliosTask
    {
        [JsonProperty("taskId")]
        public string Id { get; set; }
        [JsonProperty("title")]
        public string Subject { get; set; }
        [JsonProperty("description")]
        public string Body { get; set; }
        [JsonProperty("dueDate")]
        public DateTime DueDateTime { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; } // New, Accepted, Completed, 
        [JsonProperty("priority")]
        public string Importance { get; set; } // Low, Normal, High
        [JsonProperty("assignedTo")]
        public string AssignedTo => ApiKey;
        [JsonProperty("originatorId")]
        public string OriginatorId => ApiKey;
        [JsonProperty("lastModified")]
        public DateTime LastModified { get; set; }
        [JsonIgnore]
        public string ApiKey { get; set; }

        public HeliosTask() { }

        public HeliosTask(string id, string subject, string body, DateTime dueDateTime, string status, string importance, string apiKey)
        {
            Id = id;
            Subject = subject;
            Body = body;
            DueDateTime = dueDateTime;
            Status = status;
            Importance = importance;
            ApiKey = apiKey;
        }
    }

    public class HeliosTaskToUpdate
    {
        [JsonProperty("Id")]
        public string Id { get; set; }
        [JsonProperty("Title")]
        public string Subject { get; set; }
        [JsonProperty("Description")]
        public string Body { get; set; }
        [JsonProperty("DueDate")]
        public DateTime DueDateTime { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("Priority")]
        public string Importance { get; set; }
        [JsonProperty("AssignedTo")]
        public string AssignedTo => ApiKey;
        [JsonProperty("OriginatorId")]
        public string OriginatorId => "6120C583-B849-46FD-8FCE-6F3EDED245C7"; //TODO: hardcode
        [JsonProperty("Executor")]
        public string Executor => ApiKey;
        [JsonIgnore]
        public string ApiKey { get; set; }

        public HeliosTaskToUpdate() { }

        public HeliosTaskToUpdate(HeliosTask task)
        {
            Id = task.Id;
            Subject = task.Subject;
            Body = task.Body;
            DueDateTime = task.DueDateTime;
            Status = task.Status;
            Importance = task.Importance;
            ApiKey = task.ApiKey;
        }
    }

    public class HeliosTaskToCreate
    {
        [JsonProperty("Title")]
        public string Subject { get; set; }
        [JsonProperty("Description")]
        public string Body { get; set; }
        [JsonProperty("DueDate")]
        public DateTime DueDateTime { get; set; }
        [JsonProperty("Priority")]
        public string Importance { get; set; }
        [JsonProperty("OriginatorId")]
        public string OriginatorId => "6120C583-B849-46FD-8FCE-6F3EDED245C7"; //TODO hardcode
        [JsonProperty("AssignedTo")]
        public string AssignedTo => ApiKey;
        [JsonProperty("AuthorId")]
        public string AuthorId => ApiKey;
        [JsonIgnore]
        public string ApiKey { get; set; }

        public HeliosTaskToCreate()
        {
        }

        public HeliosTaskToCreate(HeliosTask task)
        {
            Subject = task.Subject;
            Body = task.Body;
            DueDateTime = task.DueDateTime;
            Importance = task.Importance;
            ApiKey = task.ApiKey;
        }
    }
}
