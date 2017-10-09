namespace Helios.Api.Domain.Entities.MainModule
{
    public class User
    {
        public int Id { get; set; }
        public string EntityId { get; set; }
        public string ApiKey { get; set; }
        public string HeliosLogin { get; set; }
        public string HeliosPassword { get; set; }
        public string HeliosToken { get; set; }
        public string HeliosRefreshToken { get; set; }
        public string MicrosoftToken { get; set; }
        public string MicrosoftRefreshToken { get; set; }
        public string EventsSyncHash { get; set; }
        public string TasksSyncHash { get; set; }
        public bool IsSyncEnabled { get; set; }
    }
}
