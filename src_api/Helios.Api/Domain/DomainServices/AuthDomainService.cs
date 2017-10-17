using System.IO;
using System.Linq;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Helios;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Dtos.Microsoft.Api;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Helios.Api.Utils.Encryption.Providers;
using Microsoft.Extensions.Configuration;

namespace Helios.Api.Domain.DomainServices
{
    public class AuthDomainService
    {
        private readonly IConfigurationRoot _configuration;

        public AuthDomainService()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        public HeliosAuthResponceDto HeliosAuth(HeliosAuthRequestDto request)
        {
            var responce = new HeliosAuthResponceDto();
            var db = new HeliosDbContext();

            var user = db.Users.FirstOrDefault(r => r.HeliosLogin == request.Login);
            if (user != null)
            {
                responce.EntityId = user.EntityId;
                return responce;
            }

            var passwordHash = AesStringEncryptor.EncryptString(request.Password, _configuration["AesKey"]);
            var newUser = new User() { HeliosLogin = request.Login, HeliosPassword = passwordHash };
            var heliosApi = new HeliosApi(newUser, false);

            var heliosTokenResponceDto = heliosApi.RetrieveToken(_configuration["AesKey"]).Result;
            if (heliosTokenResponceDto.AccessToken == null)
            {
                responce.Error = "Wrong Credentials!";
                return responce;
            }
            newUser.HeliosToken = heliosTokenResponceDto.AccessToken;

            heliosApi = new HeliosApi(newUser, true);
            var entityIdResponceDto = heliosApi.RetrieveUserEntityId().Result;
            newUser.EntityId = entityIdResponceDto.UserEntityId;
            newUser.ApiKey = entityIdResponceDto.ApiKey;

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

            var clientId = _configuration["CliendId"];
            var clientSecret = _configuration["ClientSecret"];
            var redirectUrl = _configuration["RedirectUrl"];

            var result = new MicrosoftApi(user, false).GetRefreshTokenByCode(request.Code, clientId, clientSecret, redirectUrl).Result;

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
