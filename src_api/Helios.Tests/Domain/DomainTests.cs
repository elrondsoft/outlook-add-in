using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Helios.Api.Domain.DomainServices;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.Domain.Entities.PluginModule.Microsoft;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers.Calendar;
using Helios.Api.Utils.Helpers.ClockHelper;
using Helios.Api.Utils.Helpers.Task;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Domain
{
    [TestFixture]
    public sealed class DomainTests
    {
        private readonly IConfigurationRoot _configuration;
        private readonly User _user;

        public DomainTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("C:\\Data\\Sources\\HeliosOutlookAddid\\src_api\\Helios.Api\\appsettings.json");

            _configuration = builder.Build();

            _user = new HeliosDbContext().Users.FirstOrDefault(r => r.HeliosLogin == "outlookStefan");
            
        }

        [Test] // This is formal intergration test
        //  [Ignore("Real Database")]
        public void RefresUsersAccessTokens__ShouldWork()
        {
            var usersAmount = new HeliosDbContext().Users.Count();
            var tokenUpdated =  new ScheduleDomainService().ResreshUsersAccessTokens(_configuration["AesKey"]);
            Assert.AreEqual(usersAmount, tokenUpdated);
        }

        [Test] // This is formal intergration test
        // [Ignore("Real Database")]
        public void SynchronizationShouldWork__ShouldWork()
        {
            var heliosApi = new HeliosApi(_user, true);
            var microsoftApi = new MicrosoftApi(_user, true);

            var calendarId = new CalendarHelper(microsoftApi).CreateHeliosCalendarIfNotExists("Helios");
            var folderId = new TasksFolderHelper(microsoftApi).CreateHeliosTasksFolderIfNotExists("Helios");

            var heliosEventsBefore = heliosApi.RetrieveEvents().Result.Count;
            var outlookEventsBefore = microsoftApi.RetrieveEvents(calendarId).Result.Count;
            var heliosTasksBefore = heliosApi.RetrieveTasks().Result.Count;
            var outlookTasksBefore = microsoftApi.RetrieveTasks(folderId).Result.Count;

            var syncResult = new ScheduleDomainService().SynchronizeAll();

            var heliosEventsAfter = heliosApi.RetrieveEvents().Result.Count;
            var outlookEventsAfter = microsoftApi.RetrieveEvents(calendarId).Result.Count;
            var heliosTasksAfter = heliosApi.RetrieveTasks().Result.Count;
            var outlookTasksAfter = microsoftApi.RetrieveTasks(folderId).Result.Count;

            var str = $"HeliosEventsCreated = {syncResult.HeliosEventsCreated} \n" +
                      $"HeliosEventsUpdated = {syncResult.HeliosEventsUpdated} \n" +
                      $"HeliosEventsDeleted = {syncResult.HeliosEventsDeleted} \n\n" +

                      $"OutlookEventsCreated = {syncResult.OutlookEventsCreated} \n" +
                      $"OutlookEventsUpdated = {syncResult.OutlookEventsUpdated} \n" +
                      $"OutlookEventsDeleted = {syncResult.OutlookEventsDeleted} \n\n" +

                      $"HeliosTasksCreated = {syncResult.HeliosTasksCreated} \n" +
                      $"HeliosTasksUpdated = {syncResult.HeliosTasksUpdated} \n\n" +

                      $"OutlookTasksCreated = {syncResult.OutlookTasksCreated} \n" +
                      $"OutlookTasksUpdated = {syncResult.OutlookTasksUpdated} \n";

            Console.WriteLine(str);

            Assert.AreEqual(heliosEventsBefore, heliosEventsAfter + syncResult.HeliosEventsCreated - syncResult.HeliosEventsDeleted);
            Assert.AreEqual(outlookEventsBefore, outlookEventsAfter + syncResult.OutlookEventsCreated - syncResult.OutlookEventsDeleted);

            Assert.AreEqual(heliosTasksBefore, heliosTasksAfter + syncResult.HeliosTasksCreated - syncResult.HeliosTasksDeleted);
            Assert.AreEqual(outlookTasksBefore, outlookTasksAfter + syncResult.OutlookTasksCreated - syncResult.OutlookTasksDeleted);
        }
    }
}
