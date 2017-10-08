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

            user.IsSyncEnabled = request.IsSyncEnabled;
            responce.IsSyncEnabled = user.IsSyncEnabled;
            db.SaveChanges();

            return responce;
        }
    }
}
