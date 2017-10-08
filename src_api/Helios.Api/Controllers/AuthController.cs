using Helios.Api.Domain.DomainServices;
using Helios.Api.Domain.Dtos.Api;
using Helios.Api.Domain.Dtos.Microsoft;
using Helios.Api.Domain.Dtos.Microsoft.Api;
using Microsoft.AspNetCore.Mvc;

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
