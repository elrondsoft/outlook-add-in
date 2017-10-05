namespace Helios.Api.Domain.Entities.PluginModule.Microsoft
{
    public class OutlookCalendar
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public OutlookCalendar(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
