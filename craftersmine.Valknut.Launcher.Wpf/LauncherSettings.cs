using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher
{
    public static class LauncherSettings
    {
        /// <summary>
        /// Gets launcher title
        /// </summary>
        public static string LauncherTitle { get; } = "Valknut Launcher";
        public static string LauncherBuild { get; } = "1";

        /// <summary>
        /// Gets launcher server protocol. http - unsecured, https - secured, needs server certificate
        /// </summary>
        public static string ServerProtocol { get; } = "http";

        /// <summary>
        /// Gets launcher server hostname. Only domain, ex. example.com
        /// </summary>
        public static string ServerHostname { get; } = "localhost";

        /// <summary>
        /// Gets launcher server port. Leave 80 if you are using default port or using https
        /// </summary>
        public static int ServerPort { get; } = 80;

        /// <summary>
        /// Gets registration link
        /// </summary>
        public static string RegistrationLink { get; } = "about:blank";


        public static string GetServerAddress()
        {
            if (ServerPort == 80)
                return ServerProtocol + "://" + ServerHostname + "/valknut/";
            else return ServerProtocol + "://" + ServerHostname + ":" + ServerPort + "/valknut/";
        }
    }
}
