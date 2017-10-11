using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync.Comparer;
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
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            _clock = new FakeClock();
        }

        [Test]
        public void TasksComparer_ShouldAddTwoTasks()
        {
            /* Arrange */
            Dictionary<string, string> tasksKeyDictionary = new Dictionary<string, string>() { };

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-1", "helios-subject-1", "helios-body-1", _clock.Now, "New", "Low", _user.ApiKey)
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-1", "outlook-subject-1", new TaskBody("Text", "outlook-body-1"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now)
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
                    DueDateTime = new TaskDueDateTime(_clock.Now, "UTC")
                }
            };
            var heliosTasksToCreate = new List<HeliosTask>()
            {
                new HeliosTask(null, "outlook-subject-1", "outlook-body-1", _clock.Now, "New", "Low", _user.ApiKey)
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

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask() { Id = "helios-task-1", Subject = "helios-subject-1", Body = "helios-create-1", Status = "New", Importance = "Low", DueDateTime = _clock.Now },
                new HeliosTask() { Id = "helios-task-2", Subject = "helios-subject-2", Body = "helios-update-22", Status = "New", Importance = "Low", DueDateTime = _clock.Now, LastModified = _clock.New},
                new HeliosTask() { Id = "helios-task-3", Subject = "helios-subject-3", Body = "helios-complete-33", Status = "New", Importance = "Low", DueDateTime = _clock.Now, LastModified = _clock.New},
                new HeliosTask() { Id = "helios-task-4", Subject = "helios-subject-4", Body = "helios-complete-4", Status = "New", Importance = "Low", DueDateTime = _clock.Now, LastModified = _clock.New}
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-1", "helios-subject-1", new TaskBody("Text", "helios-create-1"), "notStarted", "Low",  new TaskDueDateTime(_clock.Now, "UTC"),  _clock.Old),
                new OutlookTask("outlook-task-2", "helios-subject-2", new TaskBody("Text", "helios-update-2"), "notStarted", "Low",  new TaskDueDateTime(_clock.Now, "UTC"),  _clock.Old),
                new OutlookTask("outlook-task-2", "helios-subject-2", new TaskBody("Text", "helios-update-2"), "notStarted", "Low",  new TaskDueDateTime(_clock.Now, "UTC"),  _clock.Old),
                new OutlookTask("outlook-task-2", "helios-subject-2", new TaskBody("Text", "helios-update-2"), "notStarted", "Low",  new TaskDueDateTime(_clock.Now, "UTC"),  _clock.Old)
            };

            /* Act */
            var heliosTasksToCreate = new List<HeliosTask>()
            {

            };
            var heliosTasksToUpdate = new List<HeliosTask>()
            {

            };

            var outlookTasksToCreate = new List<OutlookTask>()
            {
                new OutlookTask(null, "helios-subject-1", new TaskBody("Text", "helios-create-1"), "notStarted", "Low",  new TaskDueDateTime(_clock.Now, "UTC"),  _clock.Old),
            };
            var outlookTasksToUpdate = new List<OutlookTask>()
            {

            };

            var mergeResult = new EntitiesComparer(new FakeEventId(), _clock).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */
            // Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToCreate, mergeResult.OutlookTasksToCreate), true);
            // Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToCreate, mergeResult.HeliosTasksToCreate), true);
        }
    }
}
