using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public sealed class BootstrapData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("versionString")]
        public string VersionString { get; set; }
        [JsonProperty("hash", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Hash { get; set; }
        [JsonProperty("archive")]
        public string Archive { get; set; }
    }
}
