using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Api.Microsoft;
using Newtonsoft.Json;

namespace Helios.Api.Utils.Helpers.Calendar
{
    public class CalendarHelper
    {
        private readonly IMicrosoftApi _microsoftApi;

        public CalendarHelper(IMicrosoftApi microsoftApi)
        {
            _microsoftApi = microsoftApi;
        }

        private string CreateCalendar(string calendarName)
        {
            return JsonConvert.DeserializeObject<OutlookCalendar>(_microsoftApi.CreateCalendar(calendarName).Result).Id;
        }

        public string CreateHeliosCalendarIfNotExists(string calendarName)
        {
            var json =_microsoftApi.RetrieveCalendars().Result;
            var dto = JsonConvert.DeserializeObject<CalendarRootDto>(json);

            foreach (var calendar in dto.Value)
            {
                if (calendar.Name == calendarName)
                    return calendar.Id;
            }

            return CreateCalendar(calendarName);
        }
    }

    public class CalendarRootDto
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }
        public IList<OutlookCalendar> Value { get; set; }
    }
}
