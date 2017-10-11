using System;
using System.Linq;
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
            var result = _heliosApi.RetrieveToken().Result;
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
        [Ignore("Real Http")]
        public void CreateTask_ShouldWork()
        {
            var heliosTask = new HeliosTask("1", "test-task-4", "test-task-4", DateTime.Now, "New", "Low", _user.ApiKey);
            var heliosTaskToCreate = new HeliosTaskToCreate(heliosTask);
            _heliosApi.CreateTask(heliosTaskToCreate);

            var createdTask = Enumerable.FirstOrDefault<HeliosTask>(_heliosApi.RetrieveTasks().Result, r => r.Subject == heliosTask.Subject);
            Assert.AreEqual(true, createdTask != null);
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
            var heliosTask = new HeliosTask("a4be6274-9258-4f22-aca5-3280274ee0bd", "test-task-555", "test-task-4", DateTime.Now, "New", "Low", _user.ApiKey);
            var heliosTaskToUpdate = new HeliosTaskToUpdate(heliosTask);
            _heliosApi.UpdateTask(heliosTaskToUpdate);

            var updatedTask = Enumerable.FirstOrDefault<HeliosTask>(_heliosApi.RetrieveTasks().Result, r => r.Subject == heliosTaskToUpdate.Subject);

            Assert.AreEqual(true, updatedTask != null);
        }

        [Test]
        [Ignore("Real Http")]
        public void CompleteTask_ShouldWork()
        {
            
        }

        [Test]
        [Ignore("Real Http")]
        public void RejectTask_ShouldWork()
        {

        }

        #endregion
    }
}

