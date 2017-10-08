using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Helios.Api.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet, Route("")]
        public JObject Get(HttpRequestMessage req)
        {
            JObject response = new JObject();

            response["version"] = Version();
            response["configuration"] = ConfigurationReleaseOrDebug();
            response["utc"] = DateTime.UtcNow;
            
            return response;
        }

        private static string ConfigurationReleaseOrDebug()
        {
            var configuration = "Release";
            #if DEBUG
            configuration = "Debug";
            #endif
            return configuration;
        }

        private static string Version()
        {
            return Assembly.GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                .InformationalVersion;
        }
    }
}
