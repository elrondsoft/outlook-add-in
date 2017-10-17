using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helios.Api.Domain.Dtos.Api
{
    public class SyncInfoDto
    {
        public int HeliosEventsCreated { get; set; }
        public int HeliosEventsUpdated { get; set; }
        public int HeliosEventsDeleted { get; set; }
        public int OutlookEventsCreated { get; set; }
        public int OutlookEventsUpdated { get; set; }
        public int OutlookEventsDeleted { get; set; }

        public int HeliosTasksCreated { get; set; }
        public int HeliosTasksUpdated { get; set; }
        public int HeliosTasksDeleted { get; set; }
        public int OutlookTasksCreated { get; set; }
        public int OutlookTasksUpdated { get; set; }
        public int OutlookTasksDeleted { get; set; }

        public string LastSyncDateTime { get; set; }

        public SyncInfoDto()
        {
        }
    }
}
