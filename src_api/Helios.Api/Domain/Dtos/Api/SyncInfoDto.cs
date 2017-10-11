using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helios.Api.Domain.Dtos.Api
{
    public class SyncInfoDto
    {
        public int EventsCreated { get; set; }
        public int EventsUpdated { get; set; }
        public int EventsDeleted { get; set; }

        public int TasksCreated { get; set; }
        public int TasksUpdated { get; set; }
        public int TasksDeleted { get; set; }
        public string LastSyncDateTime { get; set; }

        public SyncInfoDto()
        {
        }
    }
}
