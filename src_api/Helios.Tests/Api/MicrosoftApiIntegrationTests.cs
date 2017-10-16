using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Api
{
    [TestFixture]
    public sealed class MicrosoftApiIntegrationTests
    {
        readonly MicrosoftApi _mircosoftApi;
        readonly string _tasksFolderId;
        readonly IClock _clock;

        public MicrosoftApiIntegrationTests()
        {
            var testUser = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            _mircosoftApi = new MicrosoftApi(testUser, true);
            _tasksFolderId = "AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQAuAAAAAAB2S9_P2p0aSqTDoHwqpD_7AQC7LDEdFctWS71BOyZYDneVAAEaUwdjAAA=";
            _clock = new FakeClock();
        }

        #region Tasks

        [Test]
        [Ignore("Real Http")]
        public void CreateTask__ShouldWork()
        {
            var outlookTask = new OutlookTask()
            {
                Subject = "test-task-111",
                Body = new TaskBody("Text", "test-task-111"),
                Importance = "normal",
                Status = "notStarted",
                DueDateTime = new TaskDueDateTime(DateTime.Now, "UTC")
            };

            var createdTasks = _mircosoftApi.CreateTask(_tasksFolderId, outlookTask).Result;
            Console.WriteLine(JsonConvert.SerializeObject(createdTasks));
        }

        [Test]
        public void RetrieveTasks__ShouldWork()
        {
            var tasksList = _mircosoftApi.RetrieveTasks(_tasksFolderId).Result;
            Console.WriteLine(JsonConvert.SerializeObject(tasksList));
        }

        [Test]
        // [Ignore("Real Http")]
        public void UpdateTask__ShouldWork()
        {
            var outlookTask = new OutlookTask()
            {
                Id = "AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQBGAAAAAAB2S9_P2p0aSqTDoHwqpD_7BwC7LDEdFctWS71BOyZYDneVAAEaUwdjAAC7LDEdFctWS71BOyZYDneVAAEaUw9CAAA=",
                Subject = "test-task-22",
                Body = new TaskBody("Text", "test-task-22"),
                Importance = "high",
                Status = "completed",
                DueDateTime = new TaskDueDateTime(DateTime.Now, "UTC")
            };

            var updatedTask = _mircosoftApi.UpdateTask(outlookTask).Result;
            Console.WriteLine(JsonConvert.SerializeObject(updatedTask));
        }

        [Test]
        [Ignore("Real Http")]
        public void DeleteTask__ShouldWork()
        {
            var outlookTask = new OutlookTask()
            {
                Id = "AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQBGAAAAAAB2S9_P2p0aSqTDoHwqpD_7BwC7LDEdFctWS71BOyZYDneVAAEaUwdjAAC7LDEdFctWS71BOyZYDneVAAEaUw80AAA=",
                Subject = "test-task-22",
                Body = new TaskBody("Text", "test-task-22"),
                Importance = "high",
                Status = "completed",
                DueDateTime = new TaskDueDateTime(DateTime.Now, "UTC")
            };

            var responce = _mircosoftApi.UpdateTask(outlookTask);
            Console.WriteLine(JsonConvert.SerializeObject(responce.Result));
        }

        #endregion
    }
}
