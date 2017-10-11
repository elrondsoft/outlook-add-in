using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync.Comparer.Data;

namespace Helios.Api.Utils.Sync
{
    public class SyncService
    {
        private readonly IHeliosApi _heliosApi;
        private readonly IMicrosoftApi _microsoftApi;

        public SyncService(IHeliosApi heliosApi, IMicrosoftApi microsoftApi)
        {
            _heliosApi = heliosApi;
            _microsoftApi = microsoftApi;
        }

        public Dictionary<string, string> SynchronizeOutlookEvents(string calendarId, Dictionary<string, string> eventsHash, EventsComparerResult eventsComparerResult)
        {
            foreach (var outlookEvent in eventsComparerResult.OutlookEventsToCreate)
            {
                var json = _microsoftApi.CreateEvent(calendarId, outlookEvent).Result;
                // TODO: exception if event creation failed
                var createdOutlookEvent = EventsHelper.MapOutlookEventFromJson(json);

                var temporaryEventId = createdOutlookEvent.Subject + createdOutlookEvent.Start.DateTime;
                var kvp = eventsHash.SingleOrDefault(p => p.Value == temporaryEventId);
                eventsHash[kvp.Key] = createdOutlookEvent.Id;
            }

            foreach (var outlookEvent in eventsComparerResult.OutlookEventsToUpdate)
            {
                _microsoftApi.UpdateEvent(outlookEvent);
            }

            foreach (var outlookEvent in eventsComparerResult.OutlookEventsToDelete)
            {
                _microsoftApi.DeleteEvent(outlookEvent.Id);
            }

            return eventsHash;
        }

        public Dictionary<string, string> SynchronizeHeliosEvents(Dictionary<string, string> eventsHash, IList<HeliosEvent> originalHeliosEvents, EventsComparerResult eventsComparerResult)
        {
            foreach (var heliosEvent in eventsComparerResult.HeliosEventsToCreate)
            {
                originalHeliosEvents.Add(heliosEvent);
            }

            foreach (var heliosEvent in eventsComparerResult.HeliosEventsToDelete)
            {
                var eventToDelete = originalHeliosEvents.FirstOrDefault(r => r.Id == heliosEvent.Id);
                originalHeliosEvents.Remove(eventToDelete);
            }

            if (eventsComparerResult.HeliosEventsToCreate.Count != 0 ||
                eventsComparerResult.HeliosEventsToDelete.Count != 0)
            {
                _heliosApi.UpdateEvents(originalHeliosEvents);
            }

            return eventsHash;
        }

        public Dictionary<string, string> SynchronizeOutlookTasks(string folderId, Dictionary<string, string> tasksHash, TasksComparerResult tasksComparerResult)
        {
            foreach (var outlookTask in tasksComparerResult.OutlookTasksToCreate)
            {
                var createdOutlookTask = _microsoftApi.CreateTask(folderId, outlookTask).Result;

                var temporaryTaskId = createdOutlookTask.Subject + createdOutlookTask.DueDateTime.DateTime;
                var kvp = tasksHash.SingleOrDefault(p => p.Value == temporaryTaskId);
                tasksHash[kvp.Key] = createdOutlookTask.Id;
            }

            foreach (var outlookTask in tasksComparerResult.OutlookTasksToUpdate)
            {
                _microsoftApi.UpdateTask(outlookTask);
            }

            foreach (var outlookTask in tasksComparerResult.OutlookTasksToDelete)
            {
                _microsoftApi.DeleteEvent(outlookTask.Id);
            }

            return tasksHash;
        }

        public Dictionary<string, string> SynchronizeHeliosTasks(Dictionary<string, string> tasksHash, TasksComparerResult tasksComparerResult)
        {
            foreach (var heliosTask in tasksComparerResult.HeliosTasksToCreate)
            {
                _heliosApi.CreateTask(new HeliosTaskToCreate(heliosTask));
            }

            foreach (var heliosTask in tasksComparerResult.HeliosTasksToUpdate)
            {
                _heliosApi.UpdateTask(new HeliosTaskToUpdate(heliosTask));

                if (heliosTask.Status == "Accepted")
                {
                    _heliosApi.AcceptTask(heliosTask.Id, "");
                }
                if (heliosTask.Status == "Completed")
                {
                    _heliosApi.AcceptTask(heliosTask.Id, "");
                    _heliosApi.CompleteTask(heliosTask.Id, "");
                }
            }
            
            return tasksHash;
        }
    }
}
