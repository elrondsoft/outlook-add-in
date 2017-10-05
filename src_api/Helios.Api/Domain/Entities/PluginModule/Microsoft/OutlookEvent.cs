using System;
using Newtonsoft.Json;

namespace Helios.Api.Domain.Entities.PluginModule.Microsoft
{
    public class OutlookEvent
    {
        // [JsonIgnore]
        public string Id { get; set; }
        public string Subject { get; set; }
        public EventBody Body { get; set; }
        public EventDateTime Start { get; set; }
        public EventDateTime End { get; set; }
        [JsonIgnore]
        public DateTime LastModifiedDateTime { get; set; }

        public OutlookEvent(string id, string subject, EventBody body, EventDateTime start, EventDateTime end)
        {
            Id = id;
            Subject = subject;
            Body = body;
            Start = start;
            End = end;
        }
    }

    public class EventBody
    {
        public string ContentType { get; set; }
        public string Content { get; set; }

        public EventBody(string contentType, string content)
        {
            ContentType = contentType;
            Content = content;
        }
    }

    public class EventDateTime
    {
        public DateTime DateTime { get; set; }
        public string TimeZone { get; set; }

        public EventDateTime(DateTime dateTime, string timeZone)
        {
            DateTime = dateTime;
            TimeZone = timeZone;
        }
    }
}
