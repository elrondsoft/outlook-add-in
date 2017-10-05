using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Helios;

namespace Helios.Api.Utils.Api.Helios
{
    public interface IHeliosApi
    {
        

        Task<string> RetrieveEvents();
        Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents);

        Task<string> RetrieveTasks();
        Task<string> UpdateTasks(IList<HeliosTask> heliosTasks);
    }
}
