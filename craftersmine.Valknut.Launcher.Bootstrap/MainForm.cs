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
            BootstrapData data;
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

                CheckLauncher(data);
            }
            else Fail((ErrorResponse)resp);
        }

        private void CheckLauncher(BootstrapData data)
        {
            string launcherPath = Path.Combine(dataDir, "launcher.exe");

            Version currentVer = Version.Parse(data.Version);

            if (!File.Exists(launcherPath))
                DownloadLauncher(data);
            else
            {

            }
        }

        private void DownloadLauncher(BootstrapData data)
        {
            status.Text = "Downloading launcher...";
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += launcherDownloadProgressChanged;
            webClient.DownloadDataCompleted += launcherDownloadCompleted;
        }

        private void launcherDownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void launcherDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Fail(ErrorResponse error)
        {
            using (StringReader reader = new StringReader(error.ResponseData))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ErrorResponse));
                error = (ErrorResponse)serializer.Deserialize(reader);
            }
            MessageBox.Show("Something went wrong during launcher bootstrapping!\r\nPlease try again or contact support!\r\n\r\n" + error.ErrorMessage + "\r\nStatus code: " + error.Error);
            Environment.Exit(0);
        }
    }
}
