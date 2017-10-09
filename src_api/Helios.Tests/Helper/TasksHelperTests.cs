﻿using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Task;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Helper
{
    [TestFixture]
    public sealed class TasksHelperTests
    {
        private readonly User _user;
        private readonly FakeClock _clock;

        public TasksHelperTests()
        {
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            _clock = new FakeClock();
        }

        [Test]
        public void TasksAreEquatl_ShouldWork()
        {
            var heliosTask = new HeliosTask()
            {
                Id = "task-1",
                Subject = "subject-1",
                Body = "body-1",
                Status = "New",
                Importance = "Low",
                DueDateTime = _clock.Now,

                LastModified = _clock.Now,
                OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                AuthorId = _user.ApiKey,
                AssignedTo = _user.ApiKey,
                Executor = _user.ApiKey
            };

            var outlookTask = new OutlookTask()
            {
                Id = "task-1",
                Subject = "subject-1",
                Body = new TaskBody("Text", "body-1"),
                Status = "notStarted",
                Importance = "Low",
                DueDateTime = new TaskDueDateTime(_clock.Now, "UTC")
            };


            Assert.AreEqual(TasksHelper.TasksAreEqual(heliosTask, outlookTask), true);
        }

        [Test]
        public void TaskMapping_ShouldWork()
        {
            var originalOutlookTask = new OutlookTask()
            {
                Id = "task-1",
                Subject = "subject-1",
                Body = new TaskBody("Text", "body-1"),
                Status = "notStarted",
                Importance = "Low",
                DueDateTime = new TaskDueDateTime(_clock.Now, "UTC")
            };

            var heliosTask = TasksHelper.MapToHeliosTask(originalOutlookTask.Id, originalOutlookTask, _clock, _user);
            var newOutlookTask = TasksHelper.MapToOutlookTask(heliosTask.Id, heliosTask, _clock);

            Assert.AreEqual(TasksHelper.TasksAreEqual(heliosTask, originalOutlookTask), true);
            Assert.AreEqual(JsonConvert.SerializeObject(originalOutlookTask), JsonConvert.SerializeObject(newOutlookTask));
        }
    }
}