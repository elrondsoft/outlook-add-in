namespace Helios.Api.Domain.Dtos.Api
{
    public class SyncStatusResponceDto
    {
        public bool IsSyncEnabled { get; set; }
        public string Error { get; set; }
        public string SyncInfo { get; set; }
    }
}
