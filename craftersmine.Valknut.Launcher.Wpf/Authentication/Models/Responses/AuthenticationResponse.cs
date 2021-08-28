using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models.Responses
{
    public class AuthenticationResponse : Response
    {
        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }
        [JsonProperty("avaliableProfiles")]
        public UserProfile[] AvailableProfiles { get; set; }
        [JsonProperty("selectedProfiles")]
        public UserProfile SelectedProfile { get; set; }
    }
}
