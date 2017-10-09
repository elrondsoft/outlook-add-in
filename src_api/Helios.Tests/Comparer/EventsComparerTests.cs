using System;
using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Event;
using Helios.Api.Utils.Sync.Comparer;
using NUnit.Framework;

namespace Helios.Tests.Comparer
{
    [TestFixture]
    public sealed class EventsComparerTests
    {

        [Test]
        public void ListsComparer_SlouldWork()
        {
            var list1 = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-1", null, null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-2", null, null, new DateTime(), new DateTime(), new DateTime()),
            };
            var list2 = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-1", null, null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-2", null, null, new DateTime(), new DateTime(), new DateTime()),
            };

            Assert.AreEqual(true, EventsHelper.IsListsAreEqual(list1, list2));
        }

        [Test]
        public void CompareEvents__SlouldAddEvents()
        {
            /* Arrange */
            var user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            Dictionary<string, string> eventsKeyDictionary = new Dictionary<string, string>() { };

            var heliosEvents = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-helios-1", "helios-content-1", null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-helios-2", "helios-content-2", null, new DateTime(), new DateTime(), new DateTime())
            };
            var outlookEvents = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-3", "subject-outlook-3", new EventBody("Text", "outlook-content-3"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent("outlook-id-4", "subject-outlook-4", new EventBody("Text", "outlook-content-4"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };

            /* Act */
            var outlookEventsToCreate = new List<OutlookEvent>()
            {
                new OutlookEvent(null, "subject-helios-1", new EventBody("Text", "helios-content-1"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent(null, "subject-helios-2", new EventBody("Text", "helios-content-2"), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };
            var heliosEventsToCreate = new List<HeliosEvent>()
            {
                new HeliosEvent("generated-Helios-id-1", "subject-outlook-3", "outlook-content-3", null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("generated-Helios-id-2", "subject-outlook-4", "outlook-content-4", null, new DateTime(), new DateTime(), new DateTime())
            };
            var mergeResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeEvents(heliosEvents, outlookEvents, eventsKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookEventsToCreate, mergeResult.OutlookEventsToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosEventsToCreate, mergeResult.HeliosEventsToCreate), true);
        }

        [Test]
        public void CompareEvents__SlouldMergeEvents()
        {
            /* Arrange */
            var user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            Dictionary<string, string> eventsKeyDictionary = new Dictionary<string, string>()
            {
                { "helios-id-2","outlook-id-2" },
                { "helios-id-3","outlook-id-3-nope" },
                { "outlook-id-3-nope","outlook-id-3" }
            };

            var heliosEvents = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-1", "subject-helios-1", "", null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-2", "subject-helios-22", "", null, new DateTime(), new DateTime(), new DateTime()),
                new HeliosEvent("helios-id-3", "subject-helios-3", "", null, new DateTime(), new DateTime(), new DateTime())

            };
            var outlookEvents = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-1", "subject-outlook-1", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent("outlook-id-2", "subject-outlook-2", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
                new OutlookEvent("outlook-id-3", "subject-outlook-3", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };

            /* outlook */
            var outlookEventsToCreate = new List<OutlookEvent>()
            {
                new OutlookEvent(null, "subject-helios-1", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
            };
            var outlookEventsToUpdate = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-2", "subject-helios-22", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC")),
            };
            var outlookEventsToDelete = new List<OutlookEvent>()
            {
                new OutlookEvent("outlook-id-3", "subject-outlook-3", new EventBody("Text", ""), new EventDateTime(new DateTime(), "UTC"), new EventDateTime(new DateTime(), "UTC"))
            };
            /* helios */
            var heliosEventsToCreate = new List<HeliosEvent>()
            {
                new HeliosEvent("generated-Helios-id-1", "subject-outlook-1", "", null, new DateTime(), new DateTime(), new DateTime())
            };

            var heliosEventsToDelete = new List<HeliosEvent>()
            {
                new HeliosEvent("helios-id-3", "subject-helios-3", "", null, new DateTime(), new DateTime(), new DateTime())
            };

            /* Act */
            var mergeResult = new EntitiesComparer(new FakeEventId(), new FakeClock()).MergeEvents(heliosEvents, outlookEvents, eventsKeyDictionary);

            /* Assert */
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookEventsToCreate, mergeResult.OutlookEventsToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookEventsToUpdate, mergeResult.OutlookEventsToUpdate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(outlookEventsToDelete, mergeResult.OutlookEventsToDelete), true);

            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosEventsToCreate, mergeResult.HeliosEventsToCreate), true);
            Assert.AreEqual(EventsHelper.IsListsAreEqual(heliosEventsToDelete, mergeResult.HeliosEventsToDelete), true);
        }
    }
}
