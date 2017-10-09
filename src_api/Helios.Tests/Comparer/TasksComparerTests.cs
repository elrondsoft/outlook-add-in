using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync.Comparer;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Comparer
{
    [TestFixture]
    public sealed class TasksComparerTests
    {
        private readonly User _user;
        private readonly FakeClock _clock;

        public TasksComparerTests()
        {
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            _clock = new FakeClock();
        }

        [Test]
        public void TasksComparer_ShouldAddTwoTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>() { };
            var fakeDateTime = new DateTime();

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask()
                {
                    Id = "helios-task-1",
                    Subject = "helios-subject-1",
                    Body = "helios-body-1",
                    Status = "New",
                    Importance = "Low",
                    DueDateTime = fakeDateTime,

                    LastModified = _clock.Now,
                    OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                    AuthorId = _user.ApiKey,
                    AssignedTo = _user.ApiKey,
                    Executor = _user.ApiKey
                }
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask()
                {
                    Id = "outlook-task-1",
                    Subject = "outlook-subject-1",
                    Body = new TaskBody("Text", "outlook-body-1"),
                    Status = "notStarted",
                    Importance = "Low",
                    DueDateTime = new TaskDueDateTime(fakeDateTime, "UTC")
                }
            };

            /* Act */
            var outlookTasksToCreate = new List<OutlookTask>()
            {
                new OutlookTask()
                {
                    Id = null,
                    Subject = "helios-subject-1",
                    Body = new TaskBody("Text", "helios-body-1"),
                    Status = "notStarted",
                    Importance = "Low",
                    DueDateTime = new TaskDueDateTime(fakeDateTime, "UTC")
                }
            };
            var heliosTasksToCreate = new List<HeliosTask>()
            {
                new HeliosTask()
                {
                    Id = null,
                    Subject = "outlook-subject-1",
                    Body = "outlook-body-1",
                    Status = "New",
                    Importance = "Low",
                    DueDateTime = fakeDateTime,

                    LastModified = _clock.Now,
                    OriginatorId = "6120C583-B849-46FD-8FCE-6F3EDED245C7",
                    AuthorId = _user.ApiKey,
                    AssignedTo = _user.ApiKey,
                    Executor = _user.ApiKey
                }
            };
            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToCreate, mergeResult.OutlookTasksToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToCreate, mergeResult.HeliosTasksToCreate), true);
        }

        [Test]
        public void TasksComparer_ShouldMergeTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>() { };
            var fakeDateTime = new DateTime();

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask() { Id = "helios-task-1", Subject = "helios-subject-1", Body = "helios-body-1", Status = "New", Importance = "Low", DueDateTime = fakeDateTime }
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask() { Id = "outlook-task-1", Subject = "outlook-subject-1", Body = new TaskBody("Text", "outlook-body-1"), Status = "notStarted", Importance = "Low", DueDateTime = new TaskDueDateTime(fakeDateTime, "UTC") }
            };

            /* Act */
            var heliosTasksToCreate = new List<HeliosTask>()
            {
                new HeliosTask() { Id = "helios-task-1", Subject = "helios-subject-1", Body = "helios-body-1", Status = "New", Importance = "Low", DueDateTime = fakeDateTime }
            };
            var heliosTasksToUpdate = new List<HeliosTask>()
            {
                new HeliosTask() { Id = "helios-task-1", Subject = "helios-subject-1", Body = "helios-body-1", Status = "New", Importance = "Low", DueDateTime = fakeDateTime }
            };

            var outlookTasksToCreate = new List<OutlookTask>()
            {
                new OutlookTask() { Id = "outlook-task-1", Subject = "outlook-subject-1", Body = new TaskBody("Text", "outlook-body-1"), Status = "notStarted", Importance = "Low", DueDateTime = new TaskDueDateTime(fakeDateTime, "UTC") }
            };
            var outlookTasksToUpdate = new List<OutlookTask>()
            {
                new OutlookTask() { Id = "outlook-task-1", Subject = "outlook-subject-1", Body = new TaskBody("Text", "outlook-body-1"), Status = "notStarted", Importance = "Low", DueDateTime = new TaskDueDateTime(fakeDateTime, "UTC") }
            };
            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToCreate, mergeResult.OutlookTasksToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToCreate, mergeResult.HeliosTasksToCreate), true);
        }
    }
}
