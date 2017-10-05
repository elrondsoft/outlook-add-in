using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;

namespace Helios.Api.Utils.Sync.Comparer.Data
{
    public class EventsComparerResult
    {
        public IList<HeliosEvent> HeliosEventsToCreate { get; set; }
        public IList<HeliosEvent> HeliosEventsToUpdate { get; set; }
        public IList<HeliosEvent> HeliosEventsToDelete { get; set; }

        public IList<OutlookEvent> OutlookEventsToCreate { get; set; }
        public IList<OutlookEvent> OutlookEventsToUpdate { get; set; }
        public IList<OutlookEvent> OutlookEventsToDelete { get; set; }

        public EventsComparerResult()
        {
            HeliosEventsToCreate = new List<HeliosEvent>();
            HeliosEventsToUpdate = new List<HeliosEvent>();
            HeliosEventsToDelete = new List<HeliosEvent>();

            OutlookEventsToCreate = new List<OutlookEvent>();
            OutlookEventsToUpdate = new List<OutlookEvent>();
            OutlookEventsToDelete = new List<OutlookEvent>();
        }
    }
}
