using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Authentication.Models
{
    public sealed class MinecraftClients
    {
        [JsonProperty("clients")]
        public MinecraftClient[] Clients { get; set; }
    }

    public sealed class MinecraftFile
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }

    public class MinecraftFiles
    {
        [JsonProperty("assets")]
        public MinecraftFile AssetsArchive { get; set; }
        [JsonProperty("minecraft")]
        public MinecraftFile Minecraft { get; set; }
        [JsonProperty("libraries")]
        public MinecraftFile[] Libraries { get; set; }
        [JsonProperty("natives")]
        public MinecraftFile NativesArchive { get; set; }
        [JsonProperty("configs")]
        public MinecraftFile ConfigArchive { get; set; }
        [JsonProperty("mods")]
        public MinecraftFile[] Mods { get; set; }
    }

    public sealed class MinecraftClient
    {
        [JsonProperty("id")]
        public string ClientId { get; set; }
        [JsonProperty("version")]
        public string MinecraftVersion { get; set; }
        [JsonProperty("name")]
        public string ClientName { get; set; }
        [JsonProperty("launchString")]
        public string LaunchString { get; set; }
        [JsonProperty("clientEnabled")]
        public bool ClientEnabled { get; set; }
        [JsonProperty("assetsIndex")]
        public string AssetsIndex { get; set; }

        // Must be ignored only on server side with JsonIgnore because server loads this property dynamically. EmbedIO must include this property when serializing data to send for launcher to handle
        // However for launcher JsonIgnore must be removed and changed to JsonProperty("files") to properly deserialize data with Newtonsoft.Json
        [JsonProperty("files")]
        public MinecraftFiles Files { get; set; }
    }
}
