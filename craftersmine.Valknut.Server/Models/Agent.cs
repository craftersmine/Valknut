using Swan.Formatters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models
{
    public sealed class Agent
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
    }
}
