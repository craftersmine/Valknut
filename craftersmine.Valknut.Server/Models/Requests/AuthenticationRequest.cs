using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models.Requests
{
    public class AuthenticationRequest
    {
        [JsonProperty("agent")]
        public Agent Agent { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("clientToken")]
        public string ClientToken { get; set; }
        [JsonProperty("requestUser")]
        public bool RequestUser { get; set; }
    }
}
