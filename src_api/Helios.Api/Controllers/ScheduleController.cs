using Helios.Api.Domain.DomainServices;
using Microsoft.AspNetCore.Mvc;

namespace Helios.Api.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpPost]
        [Route("schedule/refresh-tokens")]
        public void ResreshTokens()
        {
            new ScheduleDomainService().ResreshTokens();
        }

        [HttpPost]
        [Route("schedule/sync-all")]
        public void SynchronizeAll()
        {
            new ScheduleDomainService().SynchronizeAll();
        }
    }
}

