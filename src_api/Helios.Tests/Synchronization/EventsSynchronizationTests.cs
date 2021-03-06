﻿using System;
using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync;
using Helios.Api.Utils.Sync.Comparer;
using Helios.Tests.Synchronization.Data;
using NUnit.Framework;

namespace Helios.Tests.Synchronization
{
    [TestFixture]
    public sealed class EventsSynchronizationTests
    {
        readonly FakeMicrosoftApi _microsoftApiMock;
        readonly FakeHeliosApi _heliosApiMock;
        private readonly string _calendarId;

        public EventsSynchronizationTests()
        {
            _microsoftApiMock = new FakeMicrosoftApi();
            _heliosApiMock = new FakeHeliosApi();
            _calendarId = "123";
        }

        [Test]
        public void AddTwoEvents__ShouldWork()
        {
            /* Arrange */
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
            var resultEventsKeyDictionary = new SyncService(_heliosApiMock, _microsoftApiMock).SynchronizeOutlookEvents(_calendarId, eventsKeyDictionary, eventsComparerResult);

            /* Assert */
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
