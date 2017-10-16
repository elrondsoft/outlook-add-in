using System;
using System.Linq;
using System.Threading;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Helpers.ClockHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Api
{
    [TestFixture]
    public sealed class HeliosApiIntegrationTests
    {
        private readonly User _user;
        readonly HeliosApi _heliosApi;
        private readonly FakeClock _clock;

        public HeliosApiIntegrationTests()
        {
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            _heliosApi = new HeliosApi(_user, true);
            _clock = new FakeClock();
        }

        #region User

        [Test]
        [Ignore("Real Http")]
        public void RetrieveToken__ShouldWork()
        {
            var result = _heliosApi.RetrieveToken("").Result;
            Assert.AreEqual(result.AccessToken != null, true);
        }

        [Test]
        [Ignore("Real Http")]
        public void RetrieveUserEntityId__ShouldWork()
        {
            var result = _heliosApi.RetrieveUserEntityId().Result;
            Assert.AreEqual(result.UserEntityId != null, true);
        }

        #endregion

        #region Tasks

        [Test]
        // [Ignore("Real Http")]
        public void CreateTask_ShouldWork()
        {
            var heliosTask = new HeliosTask("1", "test-task-4", "test-task-4", DateTime.Now, "New", "Low", _user.ApiKey, DateTime.Now);
            var heliosTaskToCreate = new HeliosTaskToCreate(heliosTask);
            var createdTask2 = _heliosApi.CreateTask(heliosTaskToCreate);

            Console.WriteLine(JsonConvert.SerializeObject(createdTask2.Result));


           // var createdTask = _heliosApi.RetrieveTasks().Result.FirstOrDefault(r => r.Subject == "test-task-2");
           // Assert.AreEqual(true, createdTask != null);
        }

        [Test]
        public void RetrieveTasks_ShouldWork()
        {
            var result = _heliosApi.RetrieveTasks();
            JsonConvert.SerializeObject(result);
            Assert.AreEqual(true, result.IsCompleted);
        }

        [Test]
        [Ignore("Real Http")]
        public void UpdateTask_ShouldWork()
        {
            var heliosTask = new HeliosTask("3c517f1f-e51d-4732-9544-b9006b024fe7", "test-task-33", "test-task-33", DateTime.Now, "New", "Low", _user.ApiKey, DateTime.Now);
            var heliosTaskToUpdate = new HeliosTaskToUpdate(heliosTask);
            _heliosApi.UpdateTask(heliosTaskToUpdate);

            var updatedTask = _heliosApi.RetrieveTasks().Result.FirstOrDefault(r => r.Subject == heliosTaskToUpdate.Subject);

            Assert.AreEqual(true, updatedTask != null);
        }

        [Test]
        [Ignore("Real Http")]
        public void CompleteTask_ShouldWork()
        {
            var taskId = "a6beaaba-34eb-49bb-9a28-baf5d91812cb";
            _heliosApi.CompleteTask(taskId, _user.ApiKey);
            _heliosApi.CompleteTask(taskId, _user.ApiKey);

            var status = _heliosApi.RetrieveTasks().Result.FirstOrDefault(r => r.Id == taskId).Status;
            Assert.AreEqual("completed", status.ToLower());
        }

        [Test]
        [Ignore("Real Http")]
        public void RejectTask_ShouldWork()
        {
            var taskId = "";
            _heliosApi.RejectTask(taskId, _user.ApiKey);
            _heliosApi.RejectTask(taskId, _user.ApiKey);

            var status = _heliosApi.RetrieveTasks().Result.FirstOrDefault(r => r.Id == taskId).Status;
            Assert.AreEqual("rejected", status.ToLower());
        }

        #endregion
    }
}

