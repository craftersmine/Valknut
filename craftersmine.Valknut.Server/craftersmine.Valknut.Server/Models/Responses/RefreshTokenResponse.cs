using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models.Responses
{
    public sealed class RefreshTokenResponse : Response
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }
        [JsonProperty("selectedProfile")]
        public UserProfile SelectedProfile { get; set; }
    }
}
