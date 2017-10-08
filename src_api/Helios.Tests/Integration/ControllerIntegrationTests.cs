using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helios.Api.Domain.DomainServices;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Helpers;
using NUnit.Framework;

namespace Helios.Tests.Integration
{
    [TestFixture]
    public sealed class ControllerIntegrationTests
    {
        [Test]
        // [Ignore("Real Http")]
        public void RefreshToken__ShouldWork()
        {
            new ScheduleDomainService().ResreshTokens();
        }

        [Test]
        // [Ignore("Real Http")]
        public void Synchronization__ShouldWork()
        {
            new ScheduleDomainService().SynchronizeAll();
        }
    }
}
