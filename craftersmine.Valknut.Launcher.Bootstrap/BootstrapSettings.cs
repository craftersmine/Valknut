using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public static class BootstrapSettings
    {
        public static string ServerAddress { get; } = "localhost";
        public static string ServerProtocol { get; } = "http";
        public static int ServerPort { get; } = 80;
    }
}
