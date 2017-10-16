using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Api.Microsoft;

namespace Helios.Tests.Synchronization.Data
{
    class FakeMicrosoftApi : IMicrosoftApi
    {
        private readonly TaskCompletionSource<string> _taskCompletionSource;
        private int _idItegrator;

        public FakeMicrosoftApi()
        {
            _taskCompletionSource = _taskCompletionSource = new TaskCompletionSource<string>();
            _idItegrator = 1;
        }

        public Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code, string clientId, string redirectUrl, string clientSecret)
        {
            return null;
        }

        public Task<MicrosoftRefreshTokenUpdateResponceDto> UpdateRefreshToken()
        {
            return null;
        }

        public Task<string> CreateCalendar(string calendarName)
        {
            _taskCompletionSource.SetResult("111");

            return _taskCompletionSource.Task;
        }

        public Task<string> RetrieveCalendars()
        {
            return null;
        }

        public Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent)
        {
            outlookEvent.Id = "generated-outlook-id-" + _idItegrator++;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(outlookEvent);

            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            taskCompletionSource.SetResult(json);

            return taskCompletionSource.Task;
        }

        public Task<IList<OutlookEvent>> RetrieveEvents(string calendarId)
        {
            return null;
        }

        public Task<string> UpdateEvent(OutlookEvent @event)
        {
            return null;
        }

        public Task<string> DeleteEvent(string eventId)
        {
            return null;
        }

        public Task<string> RetrieveTaskFolders()
        {
            return null;
        }

        public Task<string> CreateTaskFolder(string folderName)
        {
            return null;
        }

        public Task<OutlookTask> CreateTask(string folderId, OutlookTask task)
        {
            task.Id = "generated-outlook-task-id-" + _idItegrator++;
            
            TaskCompletionSource<OutlookTask> taskCompletionSource = new TaskCompletionSource<OutlookTask>();
            taskCompletionSource.SetResult(task);

            return taskCompletionSource.Task;
        }

        public Task<IList<OutlookTask>> RetrieveTasks(string folderId)
        {
            return null;
        }

        public Task<OutlookTask> UpdateTask(OutlookTask task)
        {
            return null;
        }

        public Task<string> DeleteTask(string taskId)
        {
            return null;
        }
    }
}