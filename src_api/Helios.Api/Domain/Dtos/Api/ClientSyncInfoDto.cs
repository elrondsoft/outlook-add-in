namespace Helios.Api.Domain.Dtos.Api
{
    public class ClientSyncInfoDto
    {
        public int EventsCreated { get; set; }
        public int EventsUpdated { get; set; }
        public int EventsDeleted { get; set; }

        public int TasksCreated { get; set; }
        public int TasksUpdated { get; set; }
        public int TasksDeleted { get; set; }

        public string LastSyncDateTime { get; set; }

        public ClientSyncInfoDto(SyncInfoDto syncInfoDto)
        {
            EventsCreated = syncInfoDto.HeliosEventsCreated + syncInfoDto.OutlookEventsCreated;
            EventsUpdated = syncInfoDto.HeliosEventsUpdated + syncInfoDto.OutlookEventsUpdated;
            EventsDeleted = syncInfoDto.HeliosEventsDeleted + syncInfoDto.OutlookEventsDeleted;

            TasksCreated = syncInfoDto.HeliosTasksCreated + syncInfoDto.OutlookTasksCreated;
            TasksUpdated = syncInfoDto.HeliosTasksUpdated + syncInfoDto.OutlookTasksUpdated;
            TasksDeleted = syncInfoDto.HeliosTasksDeleted + syncInfoDto.OutlookTasksDeleted;

            LastSyncDateTime = syncInfoDto.LastSyncDateTime;
        }
    }
}
