using System;
using System.Collections.Generic;
using System.Text;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync;
using Helios.Api.Utils.Sync.Comparer;
using Helios.Tests.Synchronization.Data;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Synchronization
{
    [TestFixture]
    public sealed class TasksSynchronizationTests
    {
        readonly FakeMicrosoftApi _microsoftApiMock;
        readonly FakeHeliosApi _heliosApiMock;
        private readonly User _user;
        private readonly FakeClock _clock;
        private readonly string _folderId;

        public TasksSynchronizationTests()
        {
            _microsoftApiMock = new FakeMicrosoftApi();
            _heliosApiMock = new FakeHeliosApi();
            _user = new User();
            _clock = new FakeClock();
            _folderId = "123";
        }
        
        [Test]
        public void TasksSynchronization_ShouldAddTwoTasks()
        {
            /* Arrange */
            var tasksHash = new Dictionary<string, string>();

            var heliosTasks = new List<HeliosTask>()
            {
                new HeliosTask("helios-task-id-1", "helios-subject-1", "helios-body-1", _clock.Now, "New", "Low", _user.ApiKey, _clock.Now),
                new HeliosTask("helios-task-id-2", "helios-subject-2", "helios-body-2", _clock.Now, "New", "Low", _user.ApiKey, _clock.Now)
            };
            var outlookTasks = new List<OutlookTask>()
            {
                new OutlookTask("outlook-task-id-1", "outlook-subject-1", new TaskBody("Text", "outlook-body-1"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now),
                new OutlookTask("outlook-task-id-2", "outlook-subject-2", new TaskBody("Text", "outlook-body-2"), "notStarted", "Low", new TaskDueDateTime(_clock.Now, "UTC"), _clock.Now)
            };

            var expectedTasksHash = new Dictionary<string, string>()
            {
                {"helios-task-id-1", "generated-outlook-task-id-1"},
                {"helios-task-id-2", "generated-outlook-task-id-2"},
                {"generated-helios-task-id-1", "outlook-task-id-1"},
                {"generated-helios-task-id-2", "outlook-task-id-2"}
            };

            /* Act */
            var comparerResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeTasks(heliosTasks, outlookTasks, _user, tasksHash);

            tasksHash = new SyncService(_heliosApiMock, _microsoftApiMock).SynchronizeOutlookTasks(_folderId, tasksHash, comparerResult);
            tasksHash = new SyncService(_heliosApiMock, _microsoftApiMock).SynchronizeHeliosTasks(tasksHash, comparerResult, _user);

            /* Assert */
            Console.WriteLine(JsonConvert.SerializeObject(expectedTasksHash));
            Console.WriteLine(JsonConvert.SerializeObject(tasksHash));
            Assert.AreEqual(true, EventsHelper.DictionariesAreEqual(expectedTasksHash, tasksHash));
        }



    }
}
