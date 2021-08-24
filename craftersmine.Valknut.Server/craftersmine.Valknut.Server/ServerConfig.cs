using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Server
{
    public sealed class ServerConfig
    {
        private ServerConfig() { }

        [JsonProperty("serverConfig")]
        public WebServerConfig WebServerConfig { get; set; }
        [JsonProperty("dbConfig")]
        public DbConnectionConfig DbConnectionConfig { get; set; }
        [JsonProperty("securityConfig")]
        public SecurityConfig SecurityConfig { get; set; }
        [JsonProperty("pathsConfig")]
        public PathsConfig PathsConfig { get; set; }

        public void SaveConfig(string path)
        {
            string conf = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, conf);
        }

        public static ServerConfig LoadConfig(string path)
        {
            string conf = File.ReadAllText(path);
            ServerConfig cfg = JsonConvert.DeserializeObject<ServerConfig>(conf);
            return cfg;
        }

        public static ServerConfig InitializeDefaultConfig()
        {
            ServerConfig cfg = new ServerConfig();
            WebServerConfig wsCfg = new WebServerConfig();
            DbConnectionConfig dbCfg = new DbConnectionConfig();
            SecurityConfig secCfg = new SecurityConfig();
            PathsConfig pathsCfg = new PathsConfig();

            wsCfg.BindAddress = "*";
            wsCfg.Port = 80;
            wsCfg.Certificate = "";
            wsCfg.EnableHttps = false;

            dbCfg.Host = "localhost";
            dbCfg.Port = 3306;
            dbCfg.Username = "admin";
            dbCfg.Password = "";
            dbCfg.Database = "valknut";

            secCfg.Secret = "CHANGE_THIS_TO_YOUR_SECRET!!!_ANY_RANDOM_STRING_WILL_WORK_BUT_DO_NOT_SHARE_THIS!!!";

            pathsCfg.ContentPath = "content/minecraft/";
            pathsCfg.EnableCapeUpload = false;

            cfg.WebServerConfig = wsCfg;
            cfg.DbConnectionConfig = dbCfg;
            cfg.SecurityConfig = secCfg;
            cfg.PathsConfig = pathsCfg;

            return cfg;
        }
    }

    public sealed class WebServerConfig
    {
        [JsonProperty("bindAddress")]
        public string BindAddress { get; set; }
        [JsonProperty("port")]
        public int Port { get; set; }
        [JsonProperty("certificateFilepath")]
        public string Certificate { get; set; }
        [JsonProperty("enavleHttps")]
        public bool EnableHttps { get; set; }
    }

    public sealed class DbConnectionConfig
    {
        [JsonProperty("dbHost")]
        public string Host { get; set; }
        [JsonProperty("dbPort")]
        public int Port { get; set; }
        [JsonProperty("dbUsername")]
        public string Username { get; set; }
        [JsonProperty("dbPassword")]
        public string Password { get; set; }
        [JsonProperty("dbName")]
        public string Database { get; set; }
    }

    public sealed class SecurityConfig
    {
        [JsonProperty("tokenSecret")]
        public string Secret { get; set; }
    }

    public sealed class PathsConfig
    {
        [JsonProperty("contentPath")]
        public string ContentPath { get; set; }
        [JsonProperty("enableCapeUpload")]
        public bool EnableCapeUpload { get; set; }
    }
}
