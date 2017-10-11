
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Helpers.Task;
using Helios.Api.Utils.Sync.Comparer.Data;

namespace Helios.Api.Utils.Sync.Comparer
{
    public class EntitiesComparer
    {
        private readonly IEventId _eventId;
        private readonly IClock _clock;

        public EntitiesComparer(IEventId eventId, IClock clock)
        {
            _eventId = eventId;
            _clock = clock;
        }

        // =======================================================================//
        // Entities comparer O(N)                                                 //
        // =======================================================================//

        // 0.  Check for Helios Calendar
        // 0.1   If calendar exists                                      -> Get it's Id
        // 0.2   Else create calendar and get it's Id

        // 1.  Get all events from Helios and Outlook

        // 1.1 Iterate over events from Helios
        // 1.2   Those who dont't exist in dictionary                    -> Create on Outlook, add to dictionary [heliosId, outlookId]
        // 1.3   Those who exist in dictionary and exist on Outlook      -> Update on Outlook if needed (Take older)
        // 1.4   Those who exist in dictionary and not exist on Outlook  -> Delete from Helios, Delete from Dictinary

        // 2.1 Iterate over events from Outlook
        // 2.2   Those who dont't exist in dictionary                    -> Create on Helios, add to dictionary [heliosId, outlookId] 
        // 2.3   Those who have heliosId and not exist on Helios         -> Delete from Outlook, Delete from Dictionary

        // =======================================================================//

        public EventsComparerResult MergeEvents(IList<HeliosEvent> heliosEvents, IList<OutlookEvent> outlookEvents, Dictionary<string, string> eventsKeyDictionary)
        {
            var result = new EventsComparerResult();

            foreach (var heliosEvent in heliosEvents)
            {
                var kpv = eventsKeyDictionary.FirstOrDefault(x => x.Key == heliosEvent.Id);
                var heliosEventId = kpv.Key;
                var outlookEventId = kpv.Value;
                var outlookEvent = outlookEvents.FirstOrDefault(r => r.Id == outlookEventId);

                // Create
                if (heliosEventId == null)
                {
                    var outlookEventToCreate = EventsHelper.MapToOutlookEvent(null, heliosEvent);
                    result.OutlookEventsToCreate.Add(outlookEventToCreate);
                    var temporaryEventId = outlookEventToCreate.Subject + outlookEventToCreate.Start.DateTime;
                    eventsKeyDictionary.Add(heliosEvent.Id, temporaryEventId);
                }

                // Update
                if (heliosEventId != null && outlookEvent != null)
                {
                    if (EventsHelper.EventsAreEqual(heliosEvent, outlookEvent))
                    {
                        continue;
                    }

                    if (EventsHelper.IsHeliosEventOlder(heliosEvent, outlookEvent))
                    {
                        result.OutlookEventsToUpdate.Add(EventsHelper.MapToOutlookEvent(outlookEvent.Id, heliosEvent));
                    }
                    else
                    {
                        result.HeliosEventsToUpdate.Add(EventsHelper.MapToHeliosEvent(heliosEvent.Id, outlookEvent, _clock));
                    }
                }

                // Delete
                if (heliosEventId != null && outlookEvent == null)
                {
                    result.HeliosEventsToDelete.Add(heliosEvent);
                    eventsKeyDictionary.Remove(heliosEvent.Id);
                }
            }

            foreach (var outlookEvent in outlookEvents)
            {
                var kpv = eventsKeyDictionary.FirstOrDefault(x => x.Value == outlookEvent.Id);
                var heliosEventId = kpv.Key;
                var outlookEventId = kpv.Value;
                var heliosEvent = heliosEvents.FirstOrDefault(r => r.Id == heliosEventId);

                // Create
                if (outlookEventId == null)
                {
                    var newHeliosEventId = _eventId.GenerateHeliosEventId();
                    var heliosEventToCreate = EventsHelper.MapToHeliosEvent(newHeliosEventId, outlookEvent, _clock);
                    result.HeliosEventsToCreate.Add(heliosEventToCreate);
                    eventsKeyDictionary.Add(newHeliosEventId, outlookEvent.Id);
                }
                
                // Delete
                if (outlookEventId != null && heliosEvent == null)
                {
                    result.OutlookEventsToDelete.Add(outlookEvent);
                    eventsKeyDictionary.Remove(heliosEventId);
                }
            }

            return result;
        }

        public TasksComparerResult MergeTasks(IList<HeliosTask> heliosTasks, IList<OutlookTask> outlookTasks, User user, Dictionary<string, string> tasksKeyDictionary)
        {
            var result = new TasksComparerResult();

            /* Helios */
            foreach (var heliosTask in heliosTasks)
            {
                var kpv = tasksKeyDictionary.FirstOrDefault(x => x.Key == heliosTask.Id);
                var heliosTaskId = kpv.Key;
                var outlookTasktId = kpv.Value;
                var outlookTask = outlookTasks.FirstOrDefault(r => r.Id == outlookTasktId);

                // Create
                if (heliosTaskId == null)
                {
                    var outlookTaskToCreate = TasksHelper.MapToOutlookTask(null, heliosTask, _clock);
                    result.OutlookTasksToCreate.Add(outlookTaskToCreate);
                    var temporaryTaskId = outlookTaskToCreate.Subject + outlookTaskToCreate.DueDateTime;
                    tasksKeyDictionary.Add(heliosTask.Id, temporaryTaskId);
                }

                // Update
                if (heliosTaskId != null && outlookTask != null)
                {
                    if (TasksHelper.TasksAreEqual(heliosTask, outlookTask))
                    {
                        continue;
                    }

                    if (TasksHelper.IsHeliosTaskOlder(heliosTask, outlookTask))
                    {
                        result.OutlookTasksToUpdate.Add(TasksHelper.MapToOutlookTask(outlookTask.Id, heliosTask, _clock));
                    }
                    else
                    {
                        result.HeliosTasksToUpdate.Add(TasksHelper.MapToHeliosTask(heliosTask.Id, outlookTask, _clock, user));
                    }
                }

                // Delete
                if (heliosTaskId != null && outlookTask == null)
                {
                    result.HeliosTasksToDelete.Add(heliosTask);
                    tasksKeyDictionary.Remove(heliosTask.Id);
                }
            }

            /* outlook */
            foreach (var outlookTask in outlookTasks)
            {
                var kpv = tasksKeyDictionary.FirstOrDefault(x => x.Value == outlookTask.Id);
                var heliosTaskId = kpv.Key;
                var outlookTaskId = kpv.Value;
                var heliosTask = heliosTasks.FirstOrDefault(r => r.Id == heliosTaskId);

                // Create
                if (outlookTaskId == null)
                {
                    var heliosTaskToCreate = TasksHelper.MapToHeliosTask(null, outlookTask, _clock, user);
                    result.HeliosTasksToCreate.Add(heliosTaskToCreate);
                    var temporaryTaskId = heliosTaskToCreate.Subject + heliosTaskToCreate.DueDateTime;
                    tasksKeyDictionary.Add(temporaryTaskId, outlookTask.Id);
                }

                // Delete
                if (outlookTaskId != null && heliosTask == null)
                {
                    result.OutlookTasksToDelete.Add(outlookTask);
                    tasksKeyDictionary.Remove(heliosTaskId);
                }
            }

            return result;
        }
    }
}
