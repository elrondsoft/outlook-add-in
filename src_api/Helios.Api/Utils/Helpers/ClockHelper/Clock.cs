using System;

namespace Helios.Api.Utils.Helpers.ClockHelper
{
    public class Clock : IClock
    {
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
