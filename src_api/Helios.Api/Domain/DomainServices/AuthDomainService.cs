using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helios.Api.Controllers;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Helios;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Dtos.Microsoft.Api;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Newtonsoft.Json;

namespace Helios.Api.Domain.DomainServices
{
    public class AuthDomainService
    {
        public HeliosAuthResponceDto HeliosAuth(HeliosAuthRequestDto request)
        {
            var responce = new HeliosAuthResponceDto();
            var db = new HeliosDbContext();

            var user = db.Users.FirstOrDefault(r => r.HeliosLogin == request.Login && r.HeliosPassword == request.Password);
            if (user != null)
            {
                responce.EntityId = user.EntityId;
                return responce;
            }

            var newUser = new User() { HeliosLogin = request.Login, HeliosPassword = request.Password };
            var heliosApi = new HeliosApi(newUser, false);

            HeliosTokenResponceDto heliosTokenResponceDto = heliosApi.RetrieveToken().Result;

            if (heliosTokenResponceDto.AccessToken == null)
            {
                responce.Error = "Wrong Credentials!";
                return responce;
            }
            newUser.HeliosToken = heliosTokenResponceDto.AccessToken;
            
            newUser.EntityId = heliosApi.RetrieveUserEntityId().Result.UserEntityId;
            responce.EntityId = newUser.EntityId;

            db.Users.Add(newUser);
            db.SaveChanges();

            return responce;
        }
        
        public MicrosoftAuthResponceDto MicrosoftAuth(MicrosoftAuthRequestDto request)
        {
            var responce = new MicrosoftAuthResponceDto();
            
            if (request.UserEntityId == null)
            {
                responce.Error = "UserEntityId is Missing";
                return responce;
            }
            var context = new HeliosDbContext();

            var user = context.Users.SingleOrDefault(r => r.EntityId == request.UserEntityId);
            if (user == null)
            {
                responce.Error = "Wrong UserID";
                return responce;
            }
            
            var result = new MicrosoftApi(user, false).GetRefreshTokenByCode(request.Code).Result;

            if (result.access_token == null)
            {
                responce.Error = "Microsoft Auth code has expired";
                return responce;
            }

            user.MicrosoftToken = result.access_token;
            user.MicrosoftRefreshToken = result.refresh_token;
            context.SaveChanges();

            responce.AccessToken = user.MicrosoftToken;
            return responce;
        }
    }
}
