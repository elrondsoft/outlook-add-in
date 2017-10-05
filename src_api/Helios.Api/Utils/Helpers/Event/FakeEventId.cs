namespace Helios.Api.Utils.Helpers.Event
{
    public class FakeEventId : IEventId
    {
        private int _idItegrator;
        public FakeEventId()
        {
            _idItegrator = 1;
        }
        public string GenerateHeliosEventId()
        {
            return "generated-Helios-id-" + _idItegrator++;
        }
    }
}
