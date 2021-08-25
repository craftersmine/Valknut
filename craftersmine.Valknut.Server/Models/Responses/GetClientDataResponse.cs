using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models.Responses
{
    public sealed class GetClientDataResponse : Response
    {
        public MinecraftClient Client { get; set; }
    }
}
