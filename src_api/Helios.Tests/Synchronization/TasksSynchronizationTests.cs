using System;
using System.Collections.Generic;
using System.Text;
using Helios.Tests.Synchronization.Data;
using NUnit.Framework;

namespace Helios.Tests.Synchronization
{
    [TestFixture]
    public sealed class TasksSynchronizationTests
    {
        readonly FakeMicrosoftApi _microsoftApiMock;
        readonly FakeHeliosApi _heliosApiMock;
        private readonly string _calendarId;

        public TasksSynchronizationTests()
        {
            _microsoftApiMock = new FakeMicrosoftApi();
            _heliosApiMock = new FakeHeliosApi();
            _calendarId = "123";
        }
        
        [Test]
        public void TasksSynchronization_ShouldWork()
        {
            
        }



    }
}
