using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
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

        public static UserSkinData FromJsonString(string data)
        {
            string json = Encoding.UTF8.GetString(Convert.FromBase64String(data));
            return JsonConvert.DeserializeObject<UserSkinData>(json);
        }
    }
}
