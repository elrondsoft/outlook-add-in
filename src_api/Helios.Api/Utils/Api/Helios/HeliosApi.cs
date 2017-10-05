﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Helios.Api.Domain.Dtos.Helios;
using Helios.Api.Domain.Dtos.Helios.Api;
using Helios.Api.Domain.Entities;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Helios;
using Helios.Api.Utils.Helpers.Event;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Helios.Api.Utils.Api.Helios
{
    public class HeliosApi : IHeliosApi
    {
        private readonly User _user;
        private readonly string _apiBaseUrl;
        private readonly string _serverToken;

        public HeliosApi(User user, bool isTokenNeeded)
        {
            _user = user;
            _apiBaseUrl = "https://helios.gunnebocloud.com";
            _serverToken = "QUVBMkUyQTQtMkU2RS00MzNFLUEyOTAtOUMzNzYyOUI3MzU5OiFIZWxpb3NXZWJBcHBQYXNzIQ==";
            if (isTokenNeeded)
            {
                CheckTokenExpiration();
            }
        }

        private void CheckTokenExpiration()
        {
            // TODO: Refactor this stub
            bool isTokenExpired = RetrieveEvents().Result.Contains("Authorization has been denied for this request");
            if (isTokenExpired)
            {
                throw new Exception("Helios access_token has expired");
            }
        }

        #region User

        public Task<HeliosTokenResponceDto> RetrieveToken()
        {
            var client = new RestClient("https://helios-api.gunnebocloud.com/auth/connect/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Basic {_serverToken}");
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=password&username={_user.HeliosLogin}&password={_user.HeliosPassword}&scope=SmartBusinessRestApi%20openid%20offline_access", ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<HeliosTokenResponceDto>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(JsonConvert.DeserializeObject<HeliosTokenResponceDto>(response.Content));
            });

            return tcs.Task;
        }

        public Task<HeliosUserEntityIdResponceDto> RetrieveUserEntityId()
        {
            var client = new RestClient("https://helios.gunnebocloud.com/webservices/api/Account/RetrieveUserEntityId");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.HeliosToken}");

            var tcs = new TaskCompletionSource<HeliosUserEntityIdResponceDto>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(new HeliosUserEntityIdResponceDto() { UserEntityId = response.Content });
            });

            return tcs.Task;
        }

        #endregion

        #region Events

        public Task<string> RetrieveEvents()
        {
            /* Compose Body */
            var body = new JObject();
            body["EntityID"] = _user.EntityId;

            var parameter = new JObject();
            parameter["Name"] = "Schedule";

            var arr = new JArray();
            arr.Add(parameter);

            body["Parameters"] = arr;

            var client = new RestClient($"{_apiBaseUrl}/webservices/api/Entity/GetPolicy");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", $"Bearer {_user.HeliosToken}");
            request.AddParameter("application/json", body.ToString(), ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> UpdateEvents(IList<HeliosEvent> heliosEvents)
        {
            var json = EventsHelper.MapFromHeliosEventsListToJson(heliosEvents);

            var client = new RestClient($"{_apiBaseUrl}/webservices/api/Entity/SetPolicy?entityId={_user.EntityId}");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("authorization", "Bearer " + _user.HeliosToken);
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        #endregion

        #region Tasks

        public Task<string> RetrieveTasks()
        {
            var client = new RestClient("https://app14.gunnebocloud.com/TaskManager/api/Task/getDailyTasks");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json;charset=UTF-8");
            request.AddHeader("authorization", "Bearer " + _user.HeliosToken);
            request.AddParameter("application/json;charset=UTF-8", "{\"OrignatorsApiKeyList\":[\"FE5D5C56-0DB3-40E1-B6EE-70C7DE5AA151\"]}", ParameterType.RequestBody);

            throw new NotImplementedException();
            // IRestResponse response = client.Execute(request);
            // return response.Content;
        }

        public Task<string> RetrieveTasksForUser(string apiKey)
        {
            var client = new RestClient("https://helios-api.gunnebocloud.com/task/api/Task/getUserAssignedTasks?assignee=" + apiKey);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", $"Bearer {_user.HeliosToken}");

            var tcs = new TaskCompletionSource<string>();
            client.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response.Content);
            });

            return tcs.Task;
        }

        public Task<string> UpdateTasks(IList<HeliosTask> heliosTasks)
        {
            var client = new RestClient("https://app14.gunnebocloud.com/TaskManager/api/Task/getDailyTasks");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json;charset=UTF-8");
            request.AddHeader("authorization", "Bearer " + _user.HeliosToken);
            request.AddParameter("application/json;charset=UTF-8", "{\"OrignatorsApiKeyList\":[\"FE5D5C56-0DB3-40E1-B6EE-70C7DE5AA151\"]}", ParameterType.RequestBody);

            throw new NotImplementedException();
            // IRestResponse response = client.Execute(request);
            // return response.Content;
        }

        #endregion
    }
}