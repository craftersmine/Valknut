using Swan;
using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models
{
    public class UserSkinData
    {
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
        [JsonProperty("profileId")]
        public string ProfileId { get; set; }
        [JsonProperty("profileName")]
        public string ProfileName { get; set; }
        [JsonProperty("textures")]
        public UserTextures Textures { get; set; }

        public string ToBase64String()
        {
            string data = this.ToJson();
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
        }
    }
}
