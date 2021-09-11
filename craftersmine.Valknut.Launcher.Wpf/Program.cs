using Swan.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.Valknut.Launcher.Wpf
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            StaticData.LauncherRootDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "." + LauncherSettings.LauncherFolder, "logs");
            if (!Directory.Exists(StaticData.LauncherRootDirectory))
                Directory.CreateDirectory(StaticData.LauncherRootDirectory);

            FileLogger fileLogger = new FileLogger(Path.Combine(StaticData.LauncherRootDirectory, "launcher.log"), false);
            Logger.RegisterLogger(fileLogger);
            Logger.Info("Initializing launcher...");
            Logger.Info("Launcher based on Valknut");
            Logger.Info("Launcher will use this address to connect: " + LauncherSettings.GetServerAddress());
            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
