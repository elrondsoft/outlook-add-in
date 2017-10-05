using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helios.Api.Domain.Dtos.Microsoft.Api
{
    public class MicrosoftAuthResponceDto
    {
        public string AccessToken { get; set; }
        public string Error { get; set; }
    }
}
