using System;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;

namespace Helios.Api.Utils.Helpers.Task
{
    public static class TasksHelper
    {
        public static bool TasksAreEqual(HeliosTask heliosEvent, OutlookTask outlookEvent)
        {
            // TODO: add datetime to compare
            if (heliosEvent.Title == outlookEvent.Subject && heliosEvent.Description == outlookEvent.Body.Content)
            {
                return true;
            }
            return false;
        }

        public static bool IsHeliosEventOlder(HeliosTask heliosEvent, OutlookTask outlookEvent)
        {
            // TODO: Implement
            return true;
            //if (heliosEvent.LastModifiedDateTime > outlookEvent.LastModifiedDateTime)
            //    return true;
            //return false;
        }

        public static HeliosTask MapToHeliosTask(string id, OutlookTask outlookEvent, IClock clock)
        {
            throw new NotImplementedException();

            // return new HeliosEvent(id, outlookEvent.Subject, outlookEvent.Body.Content, null, outlookEvent.Start.DateTime, outlookEvent.End.DateTime, clock.Now);
        }

        public static OutlookTask MapToOutlookTask(string id, HeliosTask heliosEvent)
        {
            throw new NotImplementedException();
            // return new OutlookEvent(id, heliosEvent.Title, new EventBody("Text", heliosEvent.Description), new EventDateTime(heliosEvent.Start, "UTC"), new EventDateTime(heliosEvent.End, "UTC"));
        }
    }
}
