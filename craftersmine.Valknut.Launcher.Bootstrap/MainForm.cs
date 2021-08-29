using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public partial class MainForm : Form
    {
        string address = "";
        string dataDir = "";
        BootstrapData data;

        public MainForm()
        {
            InitializeComponent();

            status.Text = "Loading Valknut Bootstrapper...";
            if (BootstrapSettings.ServerPort != 80)
                address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + ":" + BootstrapSettings.ServerPort + "/valknut/api/";
            else address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + "/valknut/api/";

            dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "." + BootstrapSettings.LauncherDir);

            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            Bootstrap();
        }

        private async void Bootstrap()
        {
            status.Text = "Downloading launcher data...";
            var resp = await HttpHelper.MakeGetRequest(address + "getBootstrapData", null);
            resp.ResponseData = resp.ResponseData
                .Trim(new char[] { '\"', '\r', '\n' });
                //.Substring(1, resp.ResponseData.Length - 1);
            if (resp.IsSuccessful)
            {
                using (StringReader reader = new StringReader(resp.ResponseData))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(BootstrapData));
                    data = (BootstrapData)serializer.Deserialize(reader);
                }

                CheckLauncher();
            }
            else Fail(resp);
        }

        private void CheckLauncher()
        {
            string launcherPath = Path.Combine(dataDir, "launcher.exe");

            Version currentVer = Version.Parse(data.Version);

            if (!File.Exists(launcherPath))
                DownloadLauncher();
            else
            {

            }
        }

        private void DownloadLauncher()
        {
            status.Text = "Downloading launcher...";
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += launcherDownloadProgressChanged;
            webClient.DownloadFileCompleted += launcherDownloadCompleted; ;
            string launcherTempArchiveDir = Path.Combine(dataDir, "temp");
            if (!Directory.Exists(launcherTempArchiveDir))
                Directory.CreateDirectory(launcherTempArchiveDir);
            string launcherTempArchive = Path.Combine(launcherTempArchiveDir, "launcher.zip");
            webClient.DownloadFileAsync(new Uri(data.Archive), launcherTempArchive);
            progress.Style = ProgressBarStyle.Continuous;
        }

        private void launcherDownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            progress.Style = ProgressBarStyle.Marquee;
            ValidateLauncherArchive();
        }

        private void ValidateLauncherArchive()
        {
            status.Text = "Validating downloaded launcher...";
        }

        private void launcherDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            status.Text = string.Format("Downloading launcher... {0}% - {1:F2} MB/{2:F2} MB", e.ProgressPercentage, e.BytesReceived / 1024f / 1024f, e.TotalBytesToReceive / 1024f / 1024f);
            progress.Value = e.ProgressPercentage;
        }

        private void Fail(Response error)
        {
            ErrorResponse err;
            using (StringReader reader = new StringReader(error.ResponseData))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorResponse));
                err = (ErrorResponse)serializer.Deserialize(reader);
            }
            MessageBox.Show("Something went wrong during launcher bootstrapping!\r\nPlease try again or contact support!\r\n\r\n" + err.ErrorMessage + "\r\nStatus code: " + err.Error);
            Environment.Exit(0);
        }
    }
}
