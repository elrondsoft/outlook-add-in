using System;
using System.Collections.Generic;
using System.Text;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Utils.Helpers.Event;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Mapping
{
    [TestFixture]
    public sealed class OutlookMappingTests
    {
        [Test]
        public void OutlookEventsJson__ShouldMapToEntityList()
        {
            var json = "[ { \"IsEntitySpecific\": false, \"IsInherited\": false, \"PossibleValues\": null, \"Details\": \"\", \"ID\": 78871, \"Type\": { \"ID\": 301, \"Name\": \"Schedule\", \"ValueTypeID\": 2, \"ValueTypeName\": \"String\", \"Description\": \"Schedule\" }, \"Value\": \"[{\\\"Id\\\":\\\"7c8e2e6a-656f-7d68-5233-71918aa82c1a\\\",\\\"Title\\\":\\\"AAA-3\\\",\\\"Description\\\":\\\"AAA\\\",\\\"Start\\\":\\\"2017-09-26T15:21:25\\\",\\\"End\\\":\\\"2017-09-26T15:21:25\\\",\\\"Category\\\":\\\"Cleaning\\\",\\\"LastModifiedDateTime\\\":\\\"2017-09-26T15:22:24\\\"},{\\\"Id\\\":\\\"7c8e2e6a656f7d68523371918aa82c1a\\\",\\\"Title\\\":\\\"AAA-4\\\",\\\"Description\\\":\\\"AAA\\\",\\\"Start\\\":\\\"2017-09-27T15:21:25\\\",\\\"End\\\":\\\"2017-09-26T15:21:25\\\",\\\"Category\\\":\\\"Cleaning\\\",\\\"LastModifiedDateTime\\\":\\\"2017-09-26T15:22:24\\\"},{\\\"id\\\":\\\"01565917-c3d5-86b2-c02b-8fbdeb8cf235\\\",\\\"title\\\":\\\"FFF\\\",\\\"description\\\":\\\"FFF\\\",\\\"start\\\":\\\"09.28.2017 11:01:39\\\",\\\"end\\\":\\\"09.28.2017 11:01:39\\\",\\\"category\\\":\\\"Cleaning\\\",\\\"lastModifiedDateTime\\\":\\\"09.28.2017 11:01:49\\\",\\\"outlookTaskId\\\":null}]\" } ]";

            IList<HeliosEvent> eventsList = EventsHelper.MapHeliosEventsListFromJson(json);

            foreach (var heliosEvent in eventsList)
            {
                //Console.WriteLine(heliosEvent.Id);
                //Console.WriteLine(heliosEvent.Category);
                //Console.WriteLine(heliosEvent.Description);
                //Console.WriteLine(heliosEvent.Start);
                //Console.WriteLine(heliosEvent.End);
                //Console.WriteLine(heliosEvent.LastModifiedDateTime);
                //Console.WriteLine("---------------");
            }

            foreach (var heliosEvent in eventsList)
            {
                //var outlookEvent = EventsHelper.MapToOutlookEvent(null, heliosEvent);
                //Console.WriteLine(outlookEvent.Id);
                //Console.WriteLine(outlookEvent.Body.ContentType);
                //Console.WriteLine(outlookEvent.Body.Content);
                //Console.WriteLine(outlookEvent.End.DateTime);
                //Console.WriteLine(outlookEvent.Start.DateTime);
                //Console.WriteLine(outlookEvent.Subject);
                //Console.WriteLine("---------------");
            }
        }
    }
}
