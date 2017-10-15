using System.Collections.Generic;
using System.Linq;
using Helios.Api.Domain.Dtos.Helios;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Helpers.ClockHelper;
using Newtonsoft.Json;

namespace Helios.Api.Utils.Helpers.Event
{
    public static class EventsHelper
    {
        public static bool EventsAreEqual(HeliosEvent heliosEvent, OutlookEvent outlookEvent)
        {
            // TODO: add datetime to compare
            if (heliosEvent.Title == outlookEvent.Subject && heliosEvent.Description == outlookEvent.Body.Content)
            {
                return true;
            }
            return false;
        }

        public static bool IsHeliosEventOlder(HeliosEvent heliosEvent, OutlookEvent outlookEvent)
        {
            if (heliosEvent.LastModifiedDateTime > outlookEvent.LastModifiedDateTime)
                return true;
            return false;
        }

        public static HeliosEvent MapToHeliosEvent(string id, OutlookEvent outlookEvent, IClock clock)
        {
            return new HeliosEvent(id, outlookEvent.Subject, outlookEvent.Body.Content, null, outlookEvent.Start.DateTime, outlookEvent.End.DateTime, clock.Now);
        }

        public static OutlookEvent MapToOutlookEvent(string id, HeliosEvent heliosEvent)
        {
            return new OutlookEvent(id, heliosEvent.Title, new EventBody("Text", heliosEvent.Description), new EventDateTime(heliosEvent.Start, "UTC"), new EventDateTime(heliosEvent.End, "UTC"));
        }

        public static string MapFromHeliosEventsListToJson(IList<HeliosEvent> heliosEvents)
        {
            var heliosEventsRootDto = new HeliosEventsRootDto();
            heliosEventsRootDto.Value = JsonConvert.SerializeObject(heliosEvents);
            heliosEventsRootDto.Type = new HeliosEventsRootDtoType() {Name = "Schedule"};

            IList<HeliosEventsRootDto> heliosEventsRootDtoList = new List<HeliosEventsRootDto>() { heliosEventsRootDto };

            return JsonConvert.SerializeObject(heliosEventsRootDtoList);
        }

        public static OutlookEvent MapOutlookEventFromJson(string json)
        {
            return JsonConvert.DeserializeObject<OutlookEvent>(json);
        }

        public static IList<OutlookEvent> MapOutlookEventsListFromJson(string json)
        {
            return JsonConvert.DeserializeObject<MicrosoftEventsRootDto>(json).Value;
        }

        public static bool DictionariesAreEqual(Dictionary<string, string> dict1, Dictionary<string, string> dict2)
        {
            return dict1.OrderBy(kvp => kvp.Key)
                .SequenceEqual(dict2.OrderBy(kvp => kvp.Key));  
        }

        public static bool IsListsAreEqual<T>(IList<T> list1, IList<T> list2)
        {
            var cnt = new Dictionary<string, int>();
            foreach (T s in list1)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(s);

                if (cnt.ContainsKey(json))
                {
                    cnt[json]++;
                }
                else
                {
                    cnt.Add(json, 1);
                }
            }
            foreach (T s in list2)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(s);
                if (cnt.ContainsKey(json))
                {
                    cnt[json]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
    }
}
