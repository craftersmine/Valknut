using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server.Models
{
    public sealed class MinecraftClients
    {
        [JsonProperty("clients"), Swan.Formatters.JsonProperty("clients")]
        public MinecraftClient[] Clients { get; set; }
    }

    public sealed class MinecraftFile
    {
        [JsonProperty("path"), Swan.Formatters.JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("hash"), Swan.Formatters.JsonProperty("hash")]
        public string Hash { get; set; }

        private MinecraftFile() { }

        public static MinecraftFile GetMinecraftFile(string path)
        {
            if (!File.Exists(path))
                return null;

            using (var sha256 = SHA256.Create())
            {
                using (var fileStream = File.OpenRead(path))
                {
                    var hash = sha256.ComputeHash(fileStream);
                    string relativePath = System.IO.Path.GetRelativePath(System.IO.Path.Combine(Program.Config.PathsConfig.ContentPath, "clients"), path);

                    string prot = "http";
                    string remotePath = "";
                    if (Program.IsHttps())
                        prot = "https";
                    if (Program.Config.WebServerConfig.Port == 80)
                        remotePath = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/clients/" + relativePath;
                    else
                        remotePath = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/clients/" + relativePath;

                    remotePath = remotePath.Replace("\\", "/");

                    return new MinecraftFile() { Hash = HashBytesToString(hash), Path = remotePath };
                }
            }
        }

        private static string HashBytesToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }

    public class MinecraftFiles
    {
        [JsonProperty("assets"), Swan.Formatters.JsonProperty("assets")]
        public MinecraftFile AssetsArchive { get; set; }
        [JsonProperty("minecraft"), Swan.Formatters.JsonProperty("minecraft")]
        public MinecraftFile Minecraft { get; set; }
        [JsonProperty("libraries"), Swan.Formatters.JsonProperty("libraries")]
        public MinecraftFile[] Libraries { get; set; }
        [JsonProperty("natives"), Swan.Formatters.JsonProperty("natives")]
        public MinecraftFile NativesArchive { get; set; }
        [JsonProperty("configs"), Swan.Formatters.JsonProperty("configs")]
        public MinecraftFile ConfigArchive { get; set; }
        [JsonProperty("mods"), Swan.Formatters.JsonProperty("mods")]
        public MinecraftFile[] Mods { get; set; }
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore), Swan.Formatters.JsonProperty("options")]
        public MinecraftFile Options { get; set; }
    }

    public sealed class MinecraftClient
    {
        [JsonProperty("id"), Swan.Formatters.JsonProperty("id")]
        public string ClientId { get; set; }
        [JsonProperty("version"), Swan.Formatters.JsonProperty("version")]
        public string MinecraftVersion { get; set; }
        [JsonProperty("name"), Swan.Formatters.JsonProperty("name")]
        public string ClientName { get; set; }
        [JsonProperty("launchString"), Swan.Formatters.JsonProperty("launchString")]
        public string LaunchString { get; set; }
        [JsonProperty("clientEnabled"), Swan.Formatters.JsonProperty("clientEnabled")]
        public bool ClientEnabled { get; set; }
        [JsonProperty("assetsIndex"), Swan.Formatters.JsonProperty("assetsIndex")]
        public string AssetsIndex { get; set; }

        // Must be ignored only on server side with JsonIgnore because server loads this property dynamically. EmbedIO must include this property when serializing data to send for launcher to handle
        // However for launcher JsonIgnore must be removed and changed to JsonProperty("files") to properly deserialize data with Newtonsoft.Json
        [JsonIgnore, Swan.Formatters.JsonProperty("files")]
        public MinecraftFiles Files { get; set; }
    }
}
