using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Helios;

namespace Helios.Api.Utils.Api.Helios
{
    public interface IHeliosApi
    {
        Task<IList<HeliosEvent>> RetrieveEvents();
        Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents);

        Task<HeliosTask> CreateTask(HeliosTaskToCreate task);
        Task<IList<HeliosTask>> RetrieveTasks();
        Task<string> UpdateTask(HeliosTaskToUpdate task);
        Task<string> AcceptTask(string taskId, string apiKey);
        void CompleteTask(string taskId, string apiKey);
        void RejectTask(string taskId, string apiKey);
    }
}
