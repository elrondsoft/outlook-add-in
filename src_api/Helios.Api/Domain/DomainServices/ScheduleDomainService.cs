using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.MainModule;
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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using static System.String;

namespace Helios.Api.Domain.DomainServices
{
    public class ScheduleDomainService
    {
        public ScheduleDomainService()
        {
        }

        public SyncInfoDto SynchronizeAll()
        {
            var db = new HeliosDbContext();
            var users = db.Users.Where(r => r.IsSyncEnabled);
            var entitiesComparer = new EntitiesComparer(new EventId(), new Clock());
            SyncInfoDto syncInfo = null;

            foreach (var user in users)
            {
                var heliosApi = new HeliosApi(user, true);
                var microsoftApi = new MicrosoftApi(user, true);
                var syncService = new SyncService(heliosApi, microsoftApi);

                // =======================================================================//
                // Events                                                                 //
                // =======================================================================//

                var calendarId = new CalendarHelper(microsoftApi).CreateHeliosCalendarIfNotExists("Helios");
                var eventsHash = new EventsHash(user).CreateEventsHashIfNotExists();

                IList<HeliosEvent> heliosEvents = heliosApi.RetrieveEvents().Result;
                IList<OutlookEvent> outlookEvents = microsoftApi.RetrieveEvents(calendarId).Result;

                EventsComparerResult eventsComparerResult = entitiesComparer.MergeEvents(heliosEvents, outlookEvents, eventsHash);

                eventsHash = syncService.SynchronizeHeliosEvents(eventsHash, heliosEvents, eventsComparerResult);
                eventsHash = syncService.SynchronizeOutlookEvents(calendarId, eventsHash, eventsComparerResult);

                user.EventsSyncHash = JsonConvert.SerializeObject(eventsHash);

                // =======================================================================//
                // Tasks                                                                  //
                // =======================================================================//

                var folderId = new TasksFolderHelper(microsoftApi).CreateHeliosTasksFolderIfNotExists("Helios");
                var tasksHash = new TasksHash(user).CreateTasksHashIfNotExists();

                IList<HeliosTask> heliosTasks = heliosApi.RetrieveTasks().Result.Where(r => r.Status.ToLower() != "rejected").ToList();
                IList<OutlookTask> outlookTasks = microsoftApi.RetrieveTasks(folderId).Result;

                TasksComparerResult tasksComparerResult = entitiesComparer.MergeTasks(heliosTasks, outlookTasks, user, tasksHash);

                // tasksHash = syncService.SynchronizeHeliosTasks(tasksHash, heliosTasks, tasksComparerResult);
                // tasksHash = syncService.SynchronizeOutlookTasks(folderId, tasksHash, tasksComparerResult);

                user.TasksSyncHash = JsonConvert.SerializeObject(tasksHash);

                // =======================================================================//
                // Sync Info                                                              //
                // =======================================================================//

                syncInfo = UpdateUserSyncInfo(eventsComparerResult, tasksComparerResult);
                user.LastUpdateInfo = JsonConvert.SerializeObject(syncInfo);
            }

            db.SaveChanges();

            return syncInfo;
        }

        private SyncInfoDto UpdateUserSyncInfo(EventsComparerResult eventsComparerResult, TasksComparerResult tasksComparerResult)
        {
            return new SyncInfoDto
            {
                HeliosEventsCreated = eventsComparerResult.HeliosEventsToCreate.Count,
                HeliosEventsUpdated = eventsComparerResult.HeliosEventsToUpdate.Count,
                HeliosEventsDeleted = eventsComparerResult.HeliosEventsToDelete.Count,
                OutlookEventsCreated = eventsComparerResult.OutlookEventsToCreate.Count,
                OutlookEventsUpdated = eventsComparerResult.OutlookEventsToUpdate.Count,
                OutlookEventsDeleted = eventsComparerResult.OutlookEventsToDelete.Count,

                HeliosTasksCreated = tasksComparerResult.HeliosTasksToCreate.Count,
                HeliosTasksUpdated = tasksComparerResult.HeliosTasksToUpdate.Count,
                HeliosTasksDeleted = tasksComparerResult.HeliosTasksToDelete.Count,
                OutlookTasksCreated = tasksComparerResult.OutlookTasksToCreate.Count,
                OutlookTasksUpdated = tasksComparerResult.OutlookTasksToUpdate.Count,
                OutlookTasksDeleted = tasksComparerResult.OutlookTasksToDelete.Count,

                LastSyncDateTime = DateTime.Now.ToString("u")
            };
        }

        public int ResreshUsersAccessTokens(string aesKey)
        {
            int tokenRefreshed = 0;
            var db = new HeliosDbContext();
            var users = db.Users;

            foreach (var user in users)
            {
                /* Microsoft */
                user.MicrosoftToken = new MicrosoftApi(user, false).UpdateRefreshToken().Result.access_token;

                /* Helios */
                user.HeliosToken = new HeliosApi(user, false).RetrieveToken(aesKey).Result.AccessToken;

                if (!IsNullOrEmpty(user.MicrosoftToken) && !IsNullOrEmpty(user.HeliosToken))
                {
                    tokenRefreshed++;
                }
            }

            db.SaveChanges();

            return tokenRefreshed;
        }
    }
}
