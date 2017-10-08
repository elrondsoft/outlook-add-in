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
                Title = "test-task-25",
                Description = "test-task-25",
                DueDate = DateTime.Now,
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                AssignedTo = _user.ApiKey,
                Priority = "Low",
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
            var testTask = new HeliosTaskToUpdate()
            {
                Id = "e8919c2d-a0c9-4151-8cf0-84243edbd4d0",
                Title = "test-task-29",
                Description = "test-task-29",
                DueDate = DateTime.Now,
                AssignedTo = _user.ApiKey,
                Priority = "Normal",
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                Executor = _user.ApiKey
            };

            var result = _heliosApi.UpdateTask(testTask);

            Assert.AreEqual(true, result.IsCompleted);
            Console.WriteLine(JsonConvert.SerializeObject(result.IsCompleted));
        }

        #endregion

    }
}

