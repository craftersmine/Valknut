using craftersmine.Valknut.Server.Models;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server
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

    public sealed class BootstrapHelper
    {
        public static void CreateDefaultBootstrapMeta()
        {
            BootstrapData bootstrapData = new BootstrapData();
            bootstrapData.Id = "valknut";
            bootstrapData.Version = 1;
            bootstrapData.VersionString = "1.0.0";
            bootstrapData.Archive = "launcher-1.0.0.zip";
            bootstrapData.Hash = null;
            string bootstrapDir = Path.Combine(Program.Config.PathsConfig.ContentPath, "bootstrap");
            string bootstrapMeta = Path.Combine(bootstrapDir, "bootstrap.json");
            string json = JsonConvert.SerializeObject(bootstrapData, Formatting.Indented);

            if (!Directory.Exists(Path.Combine(bootstrapDir, "launchers")))
                Directory.CreateDirectory(Path.Combine(bootstrapDir, "launchers"));
            if (!Directory.Exists(bootstrapDir))
                Directory.CreateDirectory(bootstrapDir);
            File.WriteAllText(bootstrapMeta, json);
        }

        public static BootstrapData GetBootstrapData()
        {
            string bootstrapDir = Path.Combine(Program.Config.PathsConfig.ContentPath, "bootstrap");
            string bootstrapMeta = Path.Combine(bootstrapDir, "bootstrap.json");
            string json = File.ReadAllText(bootstrapMeta);
            var meta = JsonConvert.DeserializeObject<BootstrapData>(json);
            string launchersDir = Path.Combine(bootstrapDir, "launchers");
            string launcherPath = Path.Combine(launchersDir, meta.Archive);

            using (var sha256 = SHA256.Create())
            {
                using (var fileStream = File.OpenRead(launcherPath))
                {
                    var hash = sha256.ComputeHash(fileStream);
                    string relativePath = Path.GetRelativePath(launchersDir, launcherPath);

                    string prot = "http";
                    string remotePath = "";
                    if (Program.IsHttps())
                        prot = "https";
                    if (Program.Config.WebServerConfig.Port == 80)
                        remotePath = prot + "://" + Program.Config.WebServerConfig.BindAddress + "/valknut/bootstrap/launchers/" + relativePath;
                    else
                        remotePath = prot + "://" + Program.Config.WebServerConfig.BindAddress + ":" + Program.Config.WebServerConfig.Port + "/valknut/bootstrap/launchers/" + relativePath;

                    remotePath = remotePath.Replace("\\", "/");

                    meta.Archive = remotePath;
                    meta.Hash = HashBytesToString(hash);
                }
            }

            return meta;
        }
        private static string HashBytesToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
