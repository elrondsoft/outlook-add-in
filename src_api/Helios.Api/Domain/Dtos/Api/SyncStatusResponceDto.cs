using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helios.Api.Domain.Dtos.Api
{
    public class SyncStatusResponceDto
    {
        public bool IsSyncEnabled { get; set; }
        public string Error { get; set; }
    }
}
