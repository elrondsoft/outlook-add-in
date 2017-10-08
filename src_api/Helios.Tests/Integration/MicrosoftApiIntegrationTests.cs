using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Microsoft;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Integration
{

    [TestFixture]
    public sealed class MicrosoftApiIntegrationTests
    {

        #region Tasks

        [Test]
        public void CreateTask__ShouldWork()
        {
            var user = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            var api = new MicrosoftApi(user, true);
            var tasksFolderId = "AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQAuAAAAAAB2S9_P2p0aSqTDoHwqpD_7AQC7LDEdFctWS71BOyZYDneVAAEaUwdjAAA=";

            // var outlookTask = new OutlookTask();


            

        }

        [Test]
        public void RetrieveTasks__ShouldWork()
        {
            var user = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            var api = new MicrosoftApi(user, true);
            var tasksFolderId = "AAMkAGNkNDRhZTBkLTIzMTItNDMzYS04MDRhLTVmMjJhNzNhMjgyNQAuAAAAAAB2S9_P2p0aSqTDoHwqpD_7AQC7LDEdFctWS71BOyZYDneVAAEaUwdjAAA=";

            IList<OutlookTask> tasks = api.RetrieveTasks(tasksFolderId).Result;

            Console.WriteLine(JsonConvert.SerializeObject(tasks));

        }

        [Test]
        public void UpdateTask__ShouldWork()
        {

        }

        [Test]
        public void CompleteTask__ShouldWork()
        {

        }

        #endregion


    }
    
}
