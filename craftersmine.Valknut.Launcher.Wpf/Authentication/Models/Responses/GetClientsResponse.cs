using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models.Responses
{
    public sealed class GetClientsResponse : Response
    {
        [JsonProperty("clientsMetadata")]
        public MinecraftClients Clients { get; set; }
    }
}
