using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Helios;

namespace Helios.Api.Utils.Api.Helios
{
    public interface IHeliosApi
    {
        Task<string> RetrieveEvents();
        Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents);

        Task<string> CreateTask(HeliosTaskToCreate task);
        Task<IList<HeliosTask>> RetrieveTasks();
        Task<string> UpdateTask(HeliosTaskToUpdate task);
        Task<string> AcceptTask(string taskId, string apiKey);
        Task<string> CompleteTask(string taskId, string apiKey);
        Task<string> RejectTask(string taskId, string apiKey);
    }
}
