using System;

namespace Helios.Api.Domain.Entities.PluginModule.Microsoft
{
    public class OutlookTask
    {
        public string Id { get; set; }
        public string Subject { get; set; }
        public TaskBody Body { get; set; }
        // public DateTime StartDateTime { get; set; }
        public DateTime DueDateTime { get; set; }

        public OutlookTask(string id, string subject, TaskBody body, DateTime dueDateTime)
        {
            Id = id;
            Subject = subject;
            Body = body;
            DueDateTime = dueDateTime;
        }
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

    public class TaskDateTime
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; }

        public TaskDateTime(DateTime dateTime, string timeZone)
        {
            DateTime = dateTime;
            TimeZone = timeZone;
        }
    }
}
