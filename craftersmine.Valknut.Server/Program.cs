using craftersmine.Valknut.Server.Controllers;
using craftersmine.Valknut.Server.Database;

using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

using Swan;
using Swan.Formatters;
using Swan.Logging;

using System;
using System.IO;
using System.Net;
using System.Text;

namespace craftersmine.Valknut.Server
{
    public static class Program
    {
        public static ServerConfig Config { get; private set; }
        public static DatabaseConnection DatabaseConnection { get; private set; }

        public static void Main(string[] args)
        {
            if (!Directory.Exists("logs/"))
                Directory.CreateDirectory("logs/");

            FileLogger fileLogger = new FileLogger("logs/valknut.log", true);
            Logger.RegisterLogger(fileLogger);

            Logger.Info("Valknut Server (c) craftersmine 2021   |   v1.0-dev");
            Logger.Info("Initializing Valknut Server...");
            if (!File.Exists("server-config.json"))
            {
                Logger.Warn("No server configuration found! It might be lost or removed, check the file to configure server properly!");
                ServerConfig.InitializeDefaultConfig().SaveConfig("server-config.json");
            }

            Logger.Info("Loading server coniguration...");
            Config = ServerConfig.LoadConfig("server-config.json");

            Logger.Info("Creating file structure...");
            if (!Directory.Exists(Config.PathsConfig.ContentPath))
                Directory.CreateDirectory(Config.PathsConfig.ContentPath);

            string skinsPath = Path.Combine(Config.PathsConfig.ContentPath, "skins");
            string capesPath = Path.Combine(Config.PathsConfig.ContentPath, "capes");
            string minecraftClientsPath = Path.Combine(Config.PathsConfig.ContentPath, "clients");
            if (!Directory.Exists(skinsPath))
                Directory.CreateDirectory(skinsPath);
            if (!Directory.Exists(capesPath))
                Directory.CreateDirectory(capesPath);
            if (!Directory.Exists(minecraftClientsPath))
                Directory.CreateDirectory(minecraftClientsPath);

            if (!File.Exists(Path.Combine(minecraftClientsPath, "clients-metadata.json")))
                ClientsHelper.CreateDefaultMetadata();

            Logger.Info("Opening MySQL database connection to " + Config.DbConnectionConfig.Host + "...");
            DatabaseConnection = new DatabaseConnection();
            DatabaseConnection.OpenConnection();

            Logger.Info("Loading Web Server at " + Config.WebServerConfig.BindAddress + ":" + Config.WebServerConfig.Port + "...");
            WebServer webServer = new WebServer()
                .WithWebApi("/valknut/auth/", c =>
                {
                    c.WithController<AuthenticationController>();
                })
                .WithWebApi("/valknut/session/", c =>
                {
                    c.WithController<GameSessionController>();
                })
                .WithWebApi("/valknut/api/", c =>
                {
                    c.WithController<ApiController>();
                })
                //.WithStaticFolder("/valknut/textures/skins/", Config.PathsConfig.SkinsPath, false)
                .WithStaticFolder("/valknut/textures/", Config.PathsConfig.ContentPath, false, c =>
                {
                    c.ContentCaching = false;
                });

            webServer.StateChanged += WebServerStateChanged;

            webServer.Start();

            Console.ReadLine();
        }

        private static void WebServerStateChanged(object sender, WebServerStateChangedEventArgs e)
        {
            Logger.Info("Valknut Web Server is " + e.NewState.Humanize() + "!");
        }

        public static bool IsHttps()
        {
            if (Config.WebServerConfig.EnableHttps)
                return true;
            return false;
        }
    }
}