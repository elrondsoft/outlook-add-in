using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync;
using Helios.Api.Utils.Sync.Comparer;
using NUnit.Framework;

namespace Helios.Tests.Synchronization
{
    class FakeHeliosApi : IHeliosApi
    {
        public Task<string> UpdateRefreshToken()
        {
            return null;
        }

        public Task<string> RetrieveEvents()
        {
            return null;
        }

        public Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents)
        {
            return null;
        }

        public Task<string> RetrieveTasks()
        {
            return null;
        }

        public Task<string> UpdateTasks(IList<HeliosTask> heliosTasks)
        {
            throw new NotImplementedException();
        }
    }

    class FakeMicrosoftApi : IMicrosoftApi
    {
        private readonly TaskCompletionSource<string> _taskCompletionSource;
        private int _idItegrator;

        public FakeMicrosoftApi()
        {
            _taskCompletionSource = _taskCompletionSource = new TaskCompletionSource<string>();
            _idItegrator = 1;
        }

        

        

        public Task<string> CreateCalendar(string calendarName)
        {
            _taskCompletionSource.SetResult("111");

            return _taskCompletionSource.Task;
        }

        public Task<string> RetrieveCalendars()
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent)
        {
            outlookEvent.Id = "generated-outlook-id-" + _idItegrator++;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(outlookEvent);

            TaskCompletionSource<string> taskCompletionSource = new TaskCompletionSource<string>();
            taskCompletionSource.SetResult(json);

            return taskCompletionSource.Task;
        }

        public Task<string> RetrieveEvents(string calendarId)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateEvent(OutlookEvent @event)
        {
            return null;
        }

        public Task<string> DeleteEvent(string eventId)
        {
            return null;
        }

        public Task<string> RetrieveTaskFolders()
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTaskFolder(string folderName)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(OutlookTask task, string folderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveTasks(string folderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveTasks()
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteTask(string taskId)
        {
            throw new NotImplementedException();
        }

        public Task<string> CreateTask(string folderId, OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code)
        {
            throw new NotImplementedException();
        }

        public Task<MicrosoftRefreshTokenUpdateResponceDto> UpdateRefreshToken()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public sealed class SynchronizationTests
    {
        [Test]
        public void AddTwoEvents__ShouldWork()
        {
            /* Arrange */
            var microsoftApiMock = new FakeMicrosoftApi();
            var heliosApiMock = new FakeHeliosApi();

            string calendarId = "123";
            var eventsKeyDictionary = new Dictionary<string, string>();

            var heliosEvents = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-helios-1", null, null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-helios-2", null, null, new DateTime(), new DateTime(), new DateTime())
            };
            var outlookEvents = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-3", "subject-outlook-3", new EventBody("Text", "Content"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent("outlook-id-4", "subject-outlook-4", new EventBody("Text", "Content"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };

            var expectedEventsKeyDictionary = new Dictionary<string, string>()
            {
                {"helios-id-1", "generated-outlook-id-1"},
                {"helios-id-2", "generated-outlook-id-2"},
                {"generated-Helios-id-1", "outlook-id-3"},
                {"generated-Helios-id-2", "outlook-id-4"}
            };

            /* Act */
            var eventsComparerResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeEvents(heliosEvents, outlookEvents, eventsKeyDictionary);

            
            var resultEventsKeyDictionary = new SyncService(heliosApiMock, microsoftApiMock).SynchronizeOutlookEvents(calendarId, eventsKeyDictionary, eventsComparerResult);

            /* Assert */


            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(expectedEventsKeyDictionary));
            Console.WriteLine("----------");
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(resultEventsKeyDictionary));

            Assert.AreEqual(true, EventsHelper.DictionariesAreEqual(expectedEventsKeyDictionary, resultEventsKeyDictionary));
        }

        [Test]
        public void AddTwoEventsWithUglyData__ShouldWork()
        {
            /* Arrange */
            var microsoftApiMock = new FakeMicrosoftApi();
            var heliosApiMock = new FakeHeliosApi();

            string calendarId = "123";
            var eventsKeyDictionary = new Dictionary<string, string>();

            var heliosEvents = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-helios-1", null, null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-helios-2", null, null, new DateTime(), new DateTime(), new DateTime())
            };
            var outlookEvents = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-3", "subject-outlook-3", new EventBody("Text", "Content"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent("outlook-id-4", "subject-outlook-4", new EventBody("Text", "Content"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };

            var expectedEventsKeyDictionary = new Dictionary<string, string>()
            {
                {"helios-id-1", "generated-outlook-id-1"},
                {"helios-id-2", "generated-outlook-id-2"},
                {"generated-Helios-id-1", "outlook-id-3"},
                {"generated-Helios-id-2", "outlook-id-4"}
            };

            /* Act */
            var eventsComparerResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeEvents(heliosEvents, outlookEvents, eventsKeyDictionary);
            var resultEventsKeyDictionary = new SyncService(heliosApiMock, microsoftApiMock).SynchronizeOutlookEvents(calendarId, eventsKeyDictionary, eventsComparerResult);

            /* Assert */
            Assert.AreEqual(true, EventsHelper.DictionariesAreEqual(expectedEventsKeyDictionary, resultEventsKeyDictionary));
        }
    }
}
