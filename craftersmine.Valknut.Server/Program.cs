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
using System.Security.Cryptography.X509Certificates;
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

            try
            {
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

                string skinsPath = Path.Combine(Config.PathsConfig.ContentPath, "textures", "skins");
                string capesPath = Path.Combine(Config.PathsConfig.ContentPath, "textures", "capes");
                string minecraftClientsPath = Path.Combine(Config.PathsConfig.ContentPath, "clients");
                string bootstrapPath = Path.Combine(Config.PathsConfig.ContentPath, "bootstrap");
                if (!Directory.Exists(skinsPath))
                    Directory.CreateDirectory(skinsPath);
                if (!Directory.Exists(capesPath))
                    Directory.CreateDirectory(capesPath);
                if (!Directory.Exists(minecraftClientsPath))
                    Directory.CreateDirectory(minecraftClientsPath);
                if (!Directory.Exists(bootstrapPath))
                    Directory.CreateDirectory(bootstrapPath);

                if (!File.Exists(Path.Combine(minecraftClientsPath, "clients-metadata.json")))
                    ClientsHelper.CreateDefaultMetadata();
                if (!File.Exists(Path.Combine(bootstrapPath, "bootstrap.xml")))
                    BootstrapHelper.CreateDefaultBootstrapMeta();

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
                    .WithStaticFolder("/valknut/", Config.PathsConfig.ContentPath, false, c =>
                    {
                        c.ContentCaching = false;
                    });

                if (Config.WebServerConfig.EnableHttps)
                {
                    X509Certificate2 cert = new X509Certificate2(Config.WebServerConfig.Certificate);
                    webServer.Options.Certificate = cert;
                }

                webServer.StateChanged += WebServerStateChanged;

                webServer.Start();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.Fatal(ex, "Server", "Valknut server has crashed!");
                WriteCrashlog(ex);
            }
        }

        private static void WriteCrashlog(Exception ex)
        {
            if (!Directory.Exists("crashlogs\\"))
                Directory.CreateDirectory("crashlogs\\");

            string[] mes = new[] { 
                "Oops!",
                "Everything exploded!",
                "You have been nuked!",
                "Here some cookies...",
                "LET'S GO DESTROY THE WORLD!",
                "Maybe there is some patches?...",
                "Luck is a failure that failed...",
                "HAMMER!",
                "HAMMERTIME!",
                "Where is my banhammer!",
                "... --- ...",
                "Servers is down, literally",
                "Boop!",
                "The Ragnarok has come!",
                "Dark beer, cattle, and cherries!",
                "No more!"
            };

            Random rnd = new Random();
            string ms = mes[rnd.Next(0, mes.Length)];

            string crashlog =
                "Valknut server has crashed!\r\n" +
                "\r\n" +
                ms + "\r\n" +
                " ========================= \r\n" +
                ParseExceptions(ex);

            string fname = "crashlog_" + DateTime.Now.ToString("ddMMyyyy_hh-mm-ss") + ".log";
            File.WriteAllText(Path.Combine("crashlogs\\", fname), crashlog);
        }

        private static string ParseExceptions(Exception ex)
        {
            string exc =
                "Exception: " + ex.GetType().ToString() + "\r\n" +
                ex.Message + "\r\n" +
                "Stacktrace: \r\n" +
                ex.StackTrace + "\r\n" +
                "Inner exception: \r\n";
            string inner = "No inner exception.";
            if (ex.InnerException is not null)
                inner = ParseExceptions(ex.InnerException);
            exc += inner;
            return exc;
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