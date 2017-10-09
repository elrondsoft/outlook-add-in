using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Integration
{
    [TestFixture]
    public sealed class HeliosApiIntegrationTests
    {
        private readonly User _user;
        readonly HeliosApi _heliosApi;
        public HeliosApiIntegrationTests()
        {
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            _heliosApi = new HeliosApi(_user, true);
        }

        #region User

        [Test]
        [Ignore("Real Http")]
        public void RetrieveToken__ShouldWork()
        {
            var result = _heliosApi.RetrieveToken().Result;

            if (result.AccessToken == null)
            {
                throw new Exception("HeliosApi: Invalid credentials");
            }

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Test]
        [Ignore("Real Http")]
        public void RetrieveUserEntityId__ShouldWork()
        {
            var result = _heliosApi.RetrieveUserEntityId().Result;

            if (result.UserEntityId == null)
            {
                throw new Exception("HeliosApi UserEntityId: Invalid credentials");
            }

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        #endregion

        #region Tasks

        [Test]
        [Ignore("Real Http")]
        public void RetrieveTasks_ShouldWork()
        {
            var list = _heliosApi.RetrieveTasks().Result;
            Console.WriteLine($"result = {JsonConvert.SerializeObject(list)}");
        }

        [Test]
        [Ignore("Real Http")]
        public void CreateTask_ShouldWork()
        {
            var testTask = new HeliosTask()
            {
                Subject = "test-task-25",
                Body = "test-task-25",
                DueDateTime = DateTime.Now,
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                AssignedTo = _user.ApiKey,
                Importance = "Low",
                AuthorId = _user.ApiKey
            };

            var result = _heliosApi.CreateTask(testTask);

            Assert.AreEqual(true, result.IsCompleted);
            Console.WriteLine($"result = {JsonConvert.SerializeObject(result)}");
        }

        [Test]
        [Ignore("Real Http")]
        public void UpdateTask_ShouldWork()
        {
            var taskToUpdate = new HeliosTask()
            {
                Id = "22c3560e-980f-4bdb-a11f-4e560699cf47",
                Subject = "test-task-3",
                Body = "test-task-3",
                DueDateTime = DateTime.Now,
                AssignedTo = _user.ApiKey,
                Importance = "Normal",
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                Executor = _user.ApiKey
            };

            _heliosApi.UpdateTask(taskToUpdate);

            var list = _heliosApi.RetrieveTasks().Result;
            var updatedTask = list.FirstOrDefault(r => r.Id == taskToUpdate.Id);

            Assert.AreEqual(taskToUpdate.Subject, updatedTask.Subject);
        }

        #endregion
    }
}

