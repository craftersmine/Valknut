using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
{
    public class UserTexture
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
