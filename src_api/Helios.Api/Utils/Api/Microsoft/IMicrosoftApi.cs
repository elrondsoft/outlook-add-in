using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;

namespace Helios.Api.Utils.Api.Microsoft
{
    public interface IMicrosoftApi
    {
        Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code);
        Task<string> UpdateRefreshToken();

        Task<string> CreateCalendar(string calendarName);
        Task<string> RetrieveCalendars();

        Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent);
        Task<string> RetrieveEvents(string calendarId);
        Task<string> UpdateEvent(OutlookEvent @event);
        Task<string> DeleteEvent(string eventId);

        Task<string> RetrieveTaskFolders();
        Task<string> CreateTaskFolder(string folderName);

        Task<string> CreateTask(string folderId, OutlookTask task);
        Task<string> RetrieveTasks(string folderId);
        Task<string> UpdateTask(OutlookTask task);
        Task<string> DeleteTask(string taskId);
    }
}
