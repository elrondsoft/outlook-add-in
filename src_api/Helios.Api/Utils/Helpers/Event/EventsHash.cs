using System;
using System.Collections.Generic;
using Helios.Api.Domain.Entities;
using Helios.Api.Domain.Entities.MainModule;
using Newtonsoft.Json;

namespace Helios.Api.Utils.Helpers.Event
{
    public class EventsHash
    {
        private readonly User _user;

        public EventsHash(User user)
        {
            _user = user;
        }

        public Dictionary<string, string> CreateEventsHashIfNotExists()
        {
            Dictionary<string, string> dict;

            if (String.IsNullOrEmpty(_user.EventsSyncHash))
            {
                dict = new Dictionary<string, string> { };
                _user.EventsSyncHash = JsonConvert.SerializeObject(dict, Formatting.Indented);
            }
            else
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(_user.EventsSyncHash);
            }

            return dict;
        }
    }
}
