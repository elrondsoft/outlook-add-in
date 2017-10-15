using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;

namespace Helios.Api.Utils.Api.Microsoft
{
    public interface IMicrosoftApi
    {
        Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code, string clientId, string redirectUrl, string clientSecret);
        Task<MicrosoftRefreshTokenUpdateResponceDto> UpdateRefreshToken();

        Task<string> CreateCalendar(string calendarName);
        Task<string> RetrieveCalendars();

        Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent);
        Task<IList<OutlookEvent>> RetrieveEvents(string calendarId);
        Task<string> UpdateEvent(OutlookEvent @event);
        Task<string> DeleteEvent(string eventId);

        Task<string> RetrieveTaskFolders();
        Task<string> CreateTaskFolder(string folderName);

        Task<OutlookTask> CreateTask(string folderId, OutlookTask task);
        Task<IList<OutlookTask>> RetrieveTasks(string folderId);
        Task<OutlookTask> UpdateTask(OutlookTask task);
        Task<string> DeleteTask(string taskId);
    }
}
