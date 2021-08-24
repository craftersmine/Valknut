using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models.Requests
{
    public sealed class RefreshValidateTokenRequest
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }
    }
}
