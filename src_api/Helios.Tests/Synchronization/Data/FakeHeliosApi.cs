using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Utils.Api.Helios;
using Newtonsoft.Json;

namespace Helios.Tests.Synchronization.Data
{
    class FakeHeliosApi : IHeliosApi
    {
        private int _idItegrator;
        public FakeHeliosApi()
        {
            _idItegrator = 1;
        }

        public Task<string> UpdateRefreshToken()
        {
            return null;
        }

        public Task<string> RetrieveEvents()
        {
            return null;
        }

        public Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents)
        {
            return null;
        }

        public Task<HeliosTask> CreateTask(HeliosTaskToCreate task)
        {
            var heliosTask = new HeliosTask();
            heliosTask.Id = "generated-helios-task-id-" + _idItegrator++;
            heliosTask.Subject = task.Subject;
            heliosTask.DueDateTime = task.DueDateTime;

            TaskCompletionSource<HeliosTask> taskCompletionSource = new TaskCompletionSource<HeliosTask>();
            taskCompletionSource.SetResult(heliosTask);

            return taskCompletionSource.Task;
        }

        public Task<IList<HeliosTask>> RetrieveTasks()
        {
            return null;
        }

        public Task<string> UpdateTask(HeliosTaskToUpdate task)
        {
            return null;
        }

        public Task<string> AcceptTask(string taskId, string apiKey)
        {
            return null;
        }

        public Task<string> CompleteTask(string taskId, string apiKey)
        {
            return null;
        }

        public Task<string> RejectTask(string taskId, string apiKey)
        {
            return null;
        }

        Task<IList<HeliosEvent>> IHeliosApi.RetrieveEvents()
        {
            return null;
        }

        void IHeliosApi.CompleteTask(string taskId, string apiKey)
        {
            throw new NotImplementedException();
        }

        void IHeliosApi.RejectTask(string taskId, string apiKey)
        {
            throw new NotImplementedException();
        }
    }
}