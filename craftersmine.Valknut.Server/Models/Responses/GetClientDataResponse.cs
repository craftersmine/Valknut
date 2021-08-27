using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models.Responses
{
    public sealed class GetClientDataResponse : Response
    {
        [JsonProperty("client")]
        public MinecraftClient Client { get; set; }
    }
}
