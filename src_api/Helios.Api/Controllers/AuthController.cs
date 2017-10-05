using System;
using System.Linq;
using System.Threading.Tasks;
using Helios.Api.Domain.DomainServices;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Helios;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Dtos.Microsoft.Api;
using Helios.Api.Domain.Entities.MainModule;
using Helios.Api.EFContext;
using Helios.Api.Utils.Api.Helios;
using Helios.Api.Utils.Api.Microsoft;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Helios.Api.Controllers
{
    public class AuthController : Controller
    {
        [HttpPost]
        [Route("auth/helios")]
        public HeliosAuthResponceDto HeliosAuth([FromBody]HeliosAuthRequestDto requestDto)
        {
            return new AuthDomainService().HeliosAuth(requestDto);
        }

        [HttpPost]
        [Route("auth/microsoft")]
        public MicrosoftAuthResponceDto MicrosoftAuth([FromBody]MicrosoftAuthRequestDto requestDto)
        {
            return new AuthDomainService().MicrosoftAuth(requestDto);
        }
    }
}
