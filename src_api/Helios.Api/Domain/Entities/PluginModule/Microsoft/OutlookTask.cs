using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Microsoft
{
    public class OutlookTask
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("importance")]
        public string Importance { get; set; }
        [JsonProperty("body")]
        public TaskBody Body { get; set; }
        [JsonProperty("dueDateTime")]
        public TaskDueDateTime DueDateTime { get; set; }

        //public OutlookTask(string id, string subject, string status, string importance,  TaskBody body, DateTime dueDateTime)
        //{
        //    Id = id;
        //    Subject = subject;
        //    Body = body;
        //    DueDateTime = dueDateTime;
        //}
    }
    
    public class TaskBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }

        public TaskBody(string contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }
    }

    public class TaskDueDateTime
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; }

        public TaskDueDateTime(DateTime dateTime, string timeZone)
        {
            DateTime = dateTime;
            TimeZone = timeZone;
        }
    }
}
