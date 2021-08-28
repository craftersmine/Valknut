using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models.Requests
{
    public class SessionJoinRequest
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("selectedProfile")]
        public string SelectedProfile { get; set; }
        [JsonProperty("serverId")]
        public string ServerId { get; set; }
    }
}
