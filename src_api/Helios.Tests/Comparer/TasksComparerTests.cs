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
        private User _user;

        public TasksComparerTests()
        {
            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
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
                    DueDateTime = fakeDateTime
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
                    DueDateTime = fakeDateTime
                }
            };
            var mergeResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeTasks(heliosTasks, outlookTasks, _user, tasksKeyDictionary);

            /* Assert */

            Console.WriteLine(JsonConvert.SerializeObject(outlookTasksToCreate));
            foreach (var task in mergeResult.OutlookTasksToCreate)
            {
                Console.WriteLine(JsonConvert.SerializeObject(task));
            }
            Console.WriteLine("-------------------- \n");

            Console.WriteLine(JsonConvert.SerializeObject(heliosTasksToCreate));
            foreach (var task in mergeResult.HeliosTasksToCreate)
            {
                Console.WriteLine(JsonConvert.SerializeObject(task));
            }
            Console.WriteLine("--------------------");


            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookTasksToCreate, mergeResult.OutlookTasksToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosTasksToCreate, mergeResult.HeliosTasksToCreate), true);
        }
    }
}
