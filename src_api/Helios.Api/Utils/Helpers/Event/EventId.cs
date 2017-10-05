using System;

namespace Helios.Api.Utils.Helpers.Event
{
    public class EventId : IEventId
    {
        public string GenerateHeliosEventId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
