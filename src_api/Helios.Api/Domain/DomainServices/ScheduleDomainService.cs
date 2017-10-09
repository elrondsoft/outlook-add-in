using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers.Calendar;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Helpers.Task;
using Helios.Api.Utils.Sync;
using Helios.Api.Utils.Sync.Comparer;
using Helios.Api.Utils.Sync.Comparer.Data;
using Newtonsoft.Json;

namespace Helios.Api.Domain.DomainServices
{
    public class ScheduleDomainService
    {
        public void SynchronizeAll()
        {
            var db = new HeliosDbContext();
            var users = db.Users.Where(r => r.IsSyncEnabled);
            var entitiesComparer = new EntitiesComparer(new EventId(), new Clock());

            foreach (var user in users)
            {
                var heliosApi = new HeliosApi(user, true);
                var microsoftApi = new MicrosoftApi(user, true);
                var syncService = new SyncService(heliosApi, microsoftApi);

                /* Events */
                var calendarId = new CalendarHelper(microsoftApi).CreateHeliosCalendarIfNotExists("Helios");
                var eventsHash = new EventsHash(user).CreateEventsHashIfNotExists();

                IList<HeliosEvent> heliosEvents = EventsHelper.MapHeliosEventsListFromJson(heliosApi.RetrieveEvents().Result);
                IList<OutlookEvent> outlookEvents = EventsHelper.MapOutlookEventsListFromJson(microsoftApi.RetrieveEvents(calendarId).Result);

                EventsComparerResult eventsComparerResult = entitiesComparer.MergeEvents(heliosEvents, outlookEvents, eventsHash);

                eventsHash = syncService.SynchronizeHeliosEvents(eventsHash, heliosEvents, eventsComparerResult);
                eventsHash = syncService.SynchronizeOutlookEvents(calendarId, eventsHash, eventsComparerResult);

                user.EventsSyncHash = JsonConvert.SerializeObject(eventsHash);
                
                /* Tasks  */
                var folderId = new TasksFolderHelper(microsoftApi).CreateHeliosTasksFolderIfNotExists("Helios");
                var tasksHash = new EventsHash(user).CreateEventsHashIfNotExists();

                IList<HeliosTask> heliosTasks = heliosApi.RetrieveTasks().Result;
                IList<OutlookTask> outlookTasks = microsoftApi.RetrieveTasks(folderId).Result;

                TasksComparerResult tasksComparerResult = entitiesComparer.MergeTasks(heliosTasks, outlookTasks, user, tasksHash);

                tasksHash = syncService.SynchronizeHeliosTasks(tasksHash, heliosTasks, tasksComparerResult);
                tasksHash = syncService.SynchronizeOutlookTasks(folderId, tasksHash, tasksComparerResult);

                user.TasksSyncHash = JsonConvert.SerializeObject(eventsHash);
            }
            db.SaveChanges();
        }

        public void ResreshTokens()
        {
            var db = new HeliosDbContext();
            var users = db.Users;

            foreach (var user in users)
            {
                /* Microsoft */
                var microsoftApi = new MicrosoftApi(user, false);
                user.MicrosoftToken = microsoftApi.UpdateRefreshToken().Result.access_token;

                /* Helios */
                var heliosApi = new HeliosApi(user, false);
                user.HeliosToken = heliosApi.RetrieveToken().Result.AccessToken;
            }

            db.SaveChanges();
        }
    }
}
