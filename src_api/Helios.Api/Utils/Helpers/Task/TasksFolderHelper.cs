using System.Collections.Generic;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.Utils.Api.Microsoft;
using Newtonsoft.Json;

namespace Helios.Api.Utils.Helpers.Task
{
    public class TasksFolderHelper
    {
        private readonly IMicrosoftApi _microsoftApi;

        public TasksFolderHelper(IMicrosoftApi microsoftApi)
        {
            _microsoftApi = microsoftApi;
        }

        private string CreateTasksFolder(string calendarName)
        {
            var json = _microsoftApi.CreateTaskFolder(calendarName).Result;
            return JsonConvert.DeserializeObject<OutlookTaskFolder>(json).Id;
        }

        public string CreateHeliosTasksFolderIfNotExists(string name)
        {
            var json = _microsoftApi.RetrieveTaskFolders().Result;
            var dto = JsonConvert.DeserializeObject<TasksFolderRootDto>(json);
            IList<OutlookTaskFolder> outlookTaskFolders = dto.Value;

            foreach (var entity in outlookTaskFolders)
            {
                if (entity.Name == name)
                    return entity.Id;
            }

            return CreateTasksFolder(name);
        }
    }

    public class TasksFolderRootDto
    {
        [JsonProperty("@odata.context")]
        public string Context { get; set; }
        public IList<OutlookTaskFolder> Value { get; set; }
    }
}
