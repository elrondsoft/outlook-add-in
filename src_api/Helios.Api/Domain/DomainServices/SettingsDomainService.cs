using System.Linq;
using Helios.Api.Controllers;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.EFContext;
using Newtonsoft.Json;

namespace Helios.Api.Domain.DomainServices
{
    public class SettingsDomainService
    {
        public SyncStatusResponceDto GetSyncStatus(SyncStatusReuqestDto request)
        {
            var responce = new SyncStatusResponceDto();

            if (request.UserEntityId == null)
            {
                responce.Error = "No UserEntityId";
                return responce;
            }

            var db = new HeliosDbContext();
            var user = db.Users.FirstOrDefault(r => r.EntityId == request.UserEntityId);

            if (user == null)
            {
                responce.Error = "User Not Exist";
                return responce;
            }

            responce.IsSyncEnabled = user.IsSyncEnabled;
            if (responce.IsSyncEnabled)
            {
                var syncInfoDto = JsonConvert.DeserializeObject<SyncInfoDto>(user.LastUpdateInfo);
                var syncInfo = new ClientSyncInfoDto(syncInfoDto);

                responce.SyncInfo = JsonConvert.SerializeObject(syncInfo);
            }
            
            return responce;
        }

        public SyncStatusResponceDto EnableBackgroundSync(SyncStatusReuqestDto request)
        {
            var responce = new SyncStatusResponceDto();

            if (request.UserEntityId == null)
            {
                responce.Error = "No UserEntityId";
                return responce;
            }

            var db = new HeliosDbContext();
            var user = db.Users.FirstOrDefault(r => r.EntityId == request.UserEntityId);

            if (user == null)
            {
                responce.Error = "User Not Exist";
                return responce;
            }

            var isSyncEnabled = request.IsSyncEnabled;
            user.IsSyncEnabled = isSyncEnabled;
            db.SaveChanges();

            if (isSyncEnabled)
            {
                var syncInfo = new ClientSyncInfoDto(new ScheduleDomainService().SynchronizeAll(user.EntityId));
                responce.SyncInfo = JsonConvert.SerializeObject(syncInfo);
            }
            else
            {
                responce.SyncInfo = null;
            }

            responce.IsSyncEnabled = user.IsSyncEnabled;

            return responce;
        }
    }
}
