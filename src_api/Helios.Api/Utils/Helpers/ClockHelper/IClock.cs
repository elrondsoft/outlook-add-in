using System;

namespace Helios.Api.Utils.Helpers.ClockHelper
{
    public interface IClock
    {
        DateTime Now { get; }
    }
}
