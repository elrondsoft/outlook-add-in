using System;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;

namespace Helios.Api.Utils.Helpers.Task
{
    public static class TasksHelper
    {
        public static bool TasksAreEqual(HeliosTask heliosTask, OutlookTask outlookTask)
        {
            if (heliosTask.Subject == outlookTask.Subject &&
                heliosTask.Body == outlookTask.Body.Content)
            {
                return true;
            }
            return false;
        }

        public static bool IsHeliosTaskOlder(HeliosTask heliosEvent, OutlookTask outlookEvent)
        {
            // TODO: Implement
            return true;
            //if (heliosEvent.LastModifiedDateTime > outlookEvent.LastModifiedDateTime)
            //    return true;
            //return false;
        }

        public static HeliosTask MapToHeliosTask(string id, OutlookTask outlookTask, IClock clock, User user)
        {
            return new HeliosTask()
            {
                Id = id,
                Subject = outlookTask.Subject,
                Body = outlookTask.Body.Content,
                DueDateTime = outlookTask.DueDateTime.DateTime,
                Importance = outlookTask.Importance,
                Status = MapToOutlookStatus(outlookTask.Status),

                LastModified = DateTime.Now,
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7", // TODO: hardcoded value
                AuthorId = user.ApiKey,
                AssignedTo = user.ApiKey,
                Executor = user.ApiKey
            };
        }

        public static OutlookTask MapToOutlookTask(string id, HeliosTask heliosTask)
        {
            var outlookTaskStatus = MapToOutlookStatus(heliosTask.Status);

            return new OutlookTask(id, heliosTask.Subject, outlookTaskStatus, heliosTask.Importance, new TaskBody("Text", heliosTask.Body), new TaskDueDateTime(heliosTask.DueDateTime, "UTC"));
        }

        private static string MapToOutlookStatus(string heliosTaskStatus)
        {
            var outlookTaskStatus = "notStarted";

            if (heliosTaskStatus == "New")
                outlookTaskStatus = "notStarted";
            if (heliosTaskStatus == "Accepted")
                outlookTaskStatus = "inProgress";
            if (heliosTaskStatus == "Completed")
                outlookTaskStatus = "completed";

            return outlookTaskStatus;
        }

        private static string MapToHeliosStatus(string outlookTaskStatus)
        {
            var heliosTaskStatus = "New";

            if (outlookTaskStatus == "notStarted")
                heliosTaskStatus = "New";
            if (outlookTaskStatus == "inProgress")
                heliosTaskStatus = "Accepted";
            if (outlookTaskStatus == "completed")
                heliosTaskStatus = "Completed";

            return heliosTaskStatus;
        }
    }
}
