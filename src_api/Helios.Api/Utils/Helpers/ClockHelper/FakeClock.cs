using System;

namespace Helios.Api.Utils.Helpers.ClockHelper
{
    public class FakeClock : IClock
    {
        public DateTime Now => new DateTime();
        public DateTime Old => new DateTime(1990, 06, 18);
        public DateTime New => new DateTime(1990, 06, 19);
    }
}
