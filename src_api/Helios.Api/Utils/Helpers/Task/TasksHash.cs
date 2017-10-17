using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.MainModule;
using Newtonsoft.Json;

namespace Helios.Api.Utils.Helpers.Task
{
    public class TasksHash
    {
        private readonly User _user;

        public TasksHash(User user)
        {
            _user = user;
        }

        public Dictionary<string, string> CreateTasksHashIfNotExists()
        {
            Dictionary<string, string> dict;
            var hash = _user.TasksSyncHash;

            if (String.IsNullOrEmpty(hash))
            {
                dict = new Dictionary<string, string> { };
            }
            else
            {
                dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(hash);
            }

            return dict;
        }
    }
}
