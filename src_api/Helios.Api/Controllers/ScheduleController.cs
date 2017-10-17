using System.IO;
using System.Linq;
using Helios.Api.Domain.DomainServices;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Api.Domain;
using Helios.Api.EFContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Helios.Api.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IConfigurationRoot _configuration;

        public ScheduleController()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }
        
        [HttpPost]
        [Route("schedule/refresh-tokens")]
        public ResreshTokensResponceDto ResreshTokens()
        {
            var refreshedTokensAmount = new ScheduleDomainService().ResreshUsersAccessTokens(_configuration["AesKey"]);

            return new ResreshTokensResponceDto() { RefreshedTokensAmount = refreshedTokensAmount };
        }

        [HttpPost]
        [Route("schedule/sync-all")]
        public SyncInfoDto SynchronizeAll()
        {
            return new ScheduleDomainService().SynchronizeAll();
        }
    }
}

