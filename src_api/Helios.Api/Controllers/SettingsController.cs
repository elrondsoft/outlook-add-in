using System.Linq;
using System.Net.Http;
using Helios.Api.Domain.DomainServices;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.EFContext;
using Helios.Api.Utils.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Helios.Api.Controllers
{
    public class SyncStatusReuqestDto
    {
        public string UserEntityId { get; set; }
        public bool IsSyncEnabled { get; set; }
    }

    public class SettingsController : Controller
    {
        [HttpPost]
        [Route("setting/get-sync-status")]
        public SyncStatusResponceDto GetSyncStatus([FromBody]SyncStatusReuqestDto request)
        {
            return new SettingsDomainService().GetSyncStatus(request);
        }

        [HttpPost]
        [Route("setting/update-sync-status")]
        public SyncStatusResponceDto EnableBackgroundSync([FromBody]SyncStatusReuqestDto request)
        {
            return new SettingsDomainService().EnableBackgroundSync(request);
        }
    }
}
