using System.Linq;
using Helios.Api.Controllers;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.EFContext;

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
            responce.SyncInfo = user.LastUpdateInfo;
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
                new ScheduleDomainService().SynchronizeAll();
                db = new HeliosDbContext();
                user = db.Users.FirstOrDefault(r => r.EntityId == request.UserEntityId);
            }
            
            responce.IsSyncEnabled = user.IsSyncEnabled;
            responce.SyncInfo = user.LastUpdateInfo;

            return responce;
        }
    }
}
