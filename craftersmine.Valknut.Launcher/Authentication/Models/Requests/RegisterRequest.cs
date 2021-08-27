using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models.Requests
{
    public sealed class RegisterRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("repeatedPassword")]
        public string RepeatedPassword { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
