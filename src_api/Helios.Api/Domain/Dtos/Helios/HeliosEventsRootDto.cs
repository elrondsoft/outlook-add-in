namespace Helios.Api.Domain.Dtos.Helios
{
    public class HeliosEventsRootDto
    {
        public HeliosEventsRootDtoType Type { get; set; }
        public string Value;
    }

    public class HeliosEventsRootDtoType
    {
        public string Name { get; set; }
    }
}
