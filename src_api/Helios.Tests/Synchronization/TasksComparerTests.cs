using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync.Comparer;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Synchronization
{
    [TestFixture]
    public sealed class TasksComparerTests
    {
        private readonly User _user;
        private readonly FakeClock _clock;

        public TasksComparerTests()
        {
            _user = new User();
            _clock = new FakeClock();
        }

        [Test]
        public void TasksComparer_ShouldAddTwoTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>() { };

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-1", "helios-subject-1", "helios-body-1", _clock.Now, "New", "Low", _user.ApiKey, _clock.Now)
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-1", "outlook-subject-1", new TaskBody("Text", "outlook-body-1"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now)
            };

            /* Act */
            var outlookTasksToCreate = new List<OutlookTask>()
            {
                new OutlookTask(null, "helios-subject-1", new TaskBody("Text", "helios-body-1"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now)
            };
            var heliosTasksToCreate = new List<HeliosTask>()
            {
                new HeliosTask(null, "outlook-subject-1", "outlook-body-1", _clock.Now, "New", "Low", _user.ApiKey, _clock.Now)
            };
            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToCreate, mergeResult.OutlookTasksToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToCreate, mergeResult.HeliosTasksToCreate), true);
        }

        [Test]
        public void TasksComparer_ShouldUpdateTwoTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>()
            {
                {"helios-task-1", "outlook-task-1"},
                {"helios-task-2", "outlook-task-2"}
            };

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-1", "helios-subject-11", "helios-body-1", _clock.Now, "Accepted", "Normal", _user.ApiKey, _clock.New),
                new HeliosTask("helios-task-2", "helios-subject-2", "helios-body-22", _clock.Now, "New", "Low", _user.ApiKey, _clock.Old)
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-1", "helios-subject-1", new TaskBody("Text", "outlook-body-1"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Old),
                new OutlookTask("outlook-task-2", "helios-subject-22", new TaskBody("Text", "outlook-body-2"), "completed", "High", new TaskDueDateTime(_clock.Now, "UTC"), _clock.New)
            };

            /* Act */
            var outlookTasksToUpdate = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-1", "helios-subject-11", new TaskBody("Text", "helios-body-1"), "inProgress", "Normal", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now)
            };
            var heliosTasksToUpdate = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-2", "helios-subject-22", "outlook-body-2", _clock.Now, "Completed", "High", _user.ApiKey, _clock.Now)
            };

            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            Console.WriteLine(JsonConvert.SerializeObject(heliosTasksToUpdate));
            Console.WriteLine(JsonConvert.SerializeObject(mergeResult.HeliosTasksToUpdate));

            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToUpdate, mergeResult.OutlookTasksToUpdate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToUpdate, mergeResult.HeliosTasksToUpdate), true);
        }

        [Test]
        public void TasksComparer_ShouldDeleteTwoTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>()
            {
                {"helios-task-1", "outlook-task-1"},
                {"helios-task-2", "outlook-task-2"}
            };

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-1", "helios-subject-11", "helios-body-1", _clock.Now, "Accepted", "Normal", _user.ApiKey, _clock.New)
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-2", "helios-subject-22", new TaskBody("Text", "outlook-body-2"), "completed", "High", new TaskDueDateTime(_clock.Now, "UTC"), _clock.New)
            };

            /* Act */
            var heliosTasksToDelete = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-1", "helios-subject-11", "helios-body-1", _clock.Now, "Accepted", "Normal", _user.ApiKey, _clock.New)
            };
            var outlookTasksToDelete = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-2", "helios-subject-22", new TaskBody("Text", "outlook-body-2"), "completed", "High", new TaskDueDateTime(_clock.Now, "UTC"), _clock.New)
            };

            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToDelete, mergeResult.OutlookTasksToDelete), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToDelete, mergeResult.HeliosTasksToDelete), true);
        }
    }
}
