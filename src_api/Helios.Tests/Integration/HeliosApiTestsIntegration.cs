using System;
using System.Linq;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Helios.Tests.Integration
{
    [TestFixture]
    public sealed class HeliosApiTestsIntegration
    {
        #region User

        [Test]
        // [Ignore("Real Http")]
        public void RetrieveToken__ShouldWork()
        {
            var user = new User() { HeliosLogin = "outlookStefan", HeliosPassword = "!Ab123456" };
            var heliosApi = new HeliosApi(user, false);
            var result = heliosApi.RetrieveToken().Result;

            if (result.AccessToken == null)
            {
                throw new Exception("HeliosApi: Invalid credentials");
            }

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        [Test]
        [Ignore("Real Http")]
        public void RetrieveUserEntityId__ShouldWork()
        {
            var user = new HeliosDbContext().Users.FirstOrDefault(r => r.Id == 1);
            var heliosApi = new HeliosApi(user, false);

            var result = heliosApi.RetrieveUserEntityId().Result;

            if (result.UserEntityId == null)
            {
                throw new Exception("HeliosApi UserEntityId: Invalid credentials");
            }

            Console.WriteLine(JsonConvert.SerializeObject(result));
        }

        #endregion
        
    }
}

