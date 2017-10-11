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

        public Task<string> CreateCalendar(string calendarName)
        {
            _taskCompletionSource.SetResult("111");

            return _taskCompletionSource.Task;
        }

        public Task<string> RetrieveCalendars()
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent)
        {
            outlookEvent.Id = "generated-outlook-id-" + _idItegrator++;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(outlookEvent);

            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            taskCompletionSource.SetResult(json);

            return taskCompletionSource.Task;
        }

        public Task<string> RetrieveEvents(string calendarId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Task<string> CreateTaskFolder(string folderName)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(OutlookTask task, string folderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveTasks(string folderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveTasks()
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(string folderId, OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<MicrosoftRefreshTokenUpdateResponceDto> UpdateRefreshToken()
        {
            throw new NotImplementedException();
        }

        Task<OutlookTask> IMicrosoftApi.CreateTask(string folderId, OutlookTask task)
        {
            throw new NotImplementedException();
        }

        Task<IList<OutlookTask>> IMicrosoftApi.RetrieveTasks(string folderId)
        {
            throw new NotImplementedException();
        }

        Task<OutlookTask> IMicrosoftApi.UpdateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }
    }
}