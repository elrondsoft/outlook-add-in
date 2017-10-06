using System;
using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Dtos.Microsoft.Api;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Helios.Api.Utils.Api.Microsoft
{
    public class MicrosoftApi : IMicrosoftApi
    {
        private readonly User _user;
        private readonly string _clientId;

        public MicrosoftApi(User user, bool isTokenNeeded)
        {
            _user = user;
            _clientId = "cd1488fa-849d-4f93-8558-f85ca902cf61";
            if (isTokenNeeded)
            {
                CheckTokenExpiration();
            }
        }

        #region Tokens

        private void CheckTokenExpiration()
        {
            var microsoftAuthErrorResponce = JsonConvert.DeserializeObject<MicrosoftAuthErrorDto>(Me().Result);

            if (microsoftAuthErrorResponce != null && microsoftAuthErrorResponce.Error != null)
            {
                throw new Exception(microsoftAuthErrorResponce.Error.Code);
            }
        }

        public Task<string> Me()
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me");
            var request = new RestRequest(Method.GET);
            request.AddHeader("postman-token", "a6ced3d7-9fe3-9061-7282-d54c29814e48");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<MicrosoftRefreshTokenByCodeDto> GetRefreshTokenByCode(string code)
        {
            var client = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            // Local
            // request.AddParameter("application/x-www-form-urlencoded", $"client_id={_clientId}&scope=https%3A%2F%2Fgraph.microsoft.com%2Fmail.read&redirect_uri=https%3A%2F%2Flocalhost%3A3000%2Fauth.html&grant_type=authorization_code&client_secret=scY9Ymn7jtGWdfvWiiedUmq&code={code}", ParameterType.RequestBody);
            // Remote
            request.AddParameter("application/x-www-form-urlencoded", $"client_id={_clientId}" + "&scope=https://graph.microsoft.com/mail.read&redirect_uri=https://dev-helios-addin.azurewebsites.net/auth.html&grant_type=authorization_code&client_secret=scY9Ymn7jtGWdfvWiiedUmq&code=" + $"{code}", ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<MicrosoftRefreshTokenByCodeDto>();

            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(JsonConvert.DeserializeObject<MicrosoftRefreshTokenByCodeDto>(response.Content));
            });

            return tcs.Task;
        }

        public Task<MicrosoftRefreshTokenUpdateResponceDto> UpdateRefreshToken()
        {
            var client = new RestClient("https://login.microsoftonline.com/common/oauth2/v2.0/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", $"client_id=cd1488fa-849d-4f93-8558-f85ca902cf61&scope=https%3A%2F%2Fgraph.microsoft.com%2Fmail.read&redirect_uri=http%3A%2F%2Flocalhost%3A3000%2Fauth.html&grant_type=refresh_token&client_secret=scY9Ymn7jtGWdfvWiiedUmq&refresh_token={_user.MicrosoftRefreshToken}", ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<MicrosoftRefreshTokenUpdateResponceDto>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(JsonConvert.DeserializeObject<MicrosoftRefreshTokenUpdateResponceDto>(response.Content));
            });

            return tcs.Task;
        }

        #endregion

        #region Calendar

        public Task<string> CreateCalendar(string calendarName)
        {
            JObject calendarJsonObject = new JObject();
            calendarJsonObject["name"] = calendarName;

            var client = new RestClient("https://graph.microsoft.com/v1.0/me/calendars");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddParameter("application/json", calendarJsonObject.ToString(), ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> RetrieveCalendars()
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/calendars");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        #endregion

        #region Events

        public Task<string> CreateEvent(string calendarId, OutlookEvent outlookEvent)
        {
            var json = JsonConvert.SerializeObject(outlookEvent);
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/calendars/" + calendarId + "/events");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> CreateEventDebug(string calendarId, string outlookEventJson)
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/calendars/AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQBGAAAAAAB2S9_P2p0aSqTDoHwqpD_7BwC7LDEdFctWS71BOyZYDneVAAAAAAEGAAC7LDEdFctWS71BOyZYDneVAACuevZ-AAA=/events");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddParameter("application/json", outlookEventJson, ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> RetrieveEvents(string calendarId)
        {
            var client = new RestClient("https://graph.microsoft.com/v1.0/me/calendars/" + calendarId + "/events?%24expand=singleValueExtendedProperties(%24filter%3Did%20eq%20'String%20%7B66f5a359-4659-4830-9070-00040ec6ac6e%7D%20Name%20Helios')");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> UpdateEvent(OutlookEvent @event)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(@event);

            var client = new RestClient("https://graph.microsoft.com/v1.0/me/events/" + @event.Id);
            var request = new RestRequest(Method.PATCH);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> DeleteEvent(string eventId)
        {
            var client = new RestClient($"https://graph.microsoft.com/v1.0/me/events/{eventId}");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        #endregion

        #region TasksFolder

        public Task<string> RetrieveTaskFolders()
        {
            CheckTokenExpiration();
            var client = new RestClient("https://graph.microsoft.com/beta/me/outlook/taskFolders");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddHeader("content-type", "application/json");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> CreateTaskFolder(string folderName)
        {
            CheckTokenExpiration();
            var client = new RestClient("https://graph.microsoft.com/beta/me/outlook/taskfolders");
            var request = new RestRequest(Method.POST);
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", "{\r\n  \"name\": \"  " + folderName + " \"\r\n}", ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        #endregion

        #region Tasks

        public Task<string> CreateTask(string folderId, OutlookTask task)
        {
            CheckTokenExpiration();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(task);

            var client = new RestClient("https://graph.microsoft.com/beta/me/outlook/taskFolders/" + folderId + "/tasks");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> RetrieveTasks(string folderId)
        {
            CheckTokenExpiration();
            var client = new RestClient("https://graph.microsoft.com/beta/me/outlook/taskFolders/" + folderId + "/tasks");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> UpdateTask(OutlookTask task)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteTask(string taskId)
        {
            CheckTokenExpiration();
            var client = new RestClient("https://graph.microsoft.com/beta/me/outlook/tasks%28%27" + taskId + "%27%29");
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.MicrosoftToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        #endregion
    }
}
