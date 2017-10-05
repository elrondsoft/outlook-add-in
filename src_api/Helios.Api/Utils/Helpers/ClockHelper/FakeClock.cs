using System;

namespace Helios.Api.Utils.Helpers.ClockHelper
{
    public class FakeClock : IClock
    {
        public DateTime Now => new DateTime();
    }
}
