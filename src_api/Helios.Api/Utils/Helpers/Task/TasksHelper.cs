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
            if (heliosTask.Subject.ToLower() == outlookTask.Subject.ToLower() &&
                heliosTask.Body.ToLower() == outlookTask.Body.Content.ToLower() &&
                heliosTask.Status.ToLower() == MapToHeliosStatus(outlookTask.Status).ToLower() &&
                heliosTask.Importance.ToLower() == outlookTask.Importance.ToLower()
                )
            {
                return true;
            }
            return false;
        }

        public static bool IsHeliosTaskOlder(HeliosTask heliosTask, OutlookTask outlookTask)
        {
            if (heliosTask.LastModified > outlookTask.LastModifiedDateTime)
            {
                return true;
            }
            return false;
        }

        public static HeliosTask MapToHeliosTask(string id, OutlookTask outlookTask, IClock clock, User user)
        {
            return new HeliosTask(id, outlookTask.Subject, outlookTask.Body.Content, outlookTask.DueDateTime.DateTime,
                MapToHeliosStatus(outlookTask.Status), MapToHeliosImportance(outlookTask.Importance), user.ApiKey, clock.Now);
        }

        public static OutlookTask MapToOutlookTask(string id, HeliosTask heliosTask, IClock clock)
        {
            var outlookTaskStatus = MapToOutlookStatus(heliosTask.Status);

            return new OutlookTask(id, heliosTask.Subject, new TaskBody("Text", heliosTask.Body), outlookTaskStatus, heliosTask.Importance, new TaskDueDateTime(heliosTask.DueDateTime, "UTC"), clock.Now);
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

        private static string MapToHeliosImportance(string outlookImportance)
        {
            var heliosTaskStatus = "Low";

            if (outlookImportance.ToLower() == "low")
                heliosTaskStatus = "Low";
            if (outlookImportance.ToLower() == "normal")
                heliosTaskStatus = "Normal";
            if (outlookImportance.ToLower() == "high")
                heliosTaskStatus = "High";

            return heliosTaskStatus;
        }
    }
}
