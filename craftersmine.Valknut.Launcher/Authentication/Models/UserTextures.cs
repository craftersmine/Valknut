using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
{
    public class UserTextures
    {
        [JsonProperty("SKIN")]
        public UserTexture Skin { get; set; }
        [JsonProperty("CAPE")]
        public UserTexture Cape { get; set; }
    }
}
