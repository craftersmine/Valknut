using craftersmine.Valknut.Launcher.Bootstrap.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
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
            try
            {
                InitializeComponent();

                Text = BootstrapSettings.BootstrapperTitle + " - 1.0";

                status.Text = Resources.Status_LoadingBootstrapper;
                if (BootstrapSettings.ServerPort != 80)
                    address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + ":" + BootstrapSettings.ServerPort + "/valknut/api/";
                else address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + "/valknut/api/";

                dataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "." + BootstrapSettings.LauncherDir);

                if (!Directory.Exists(dataDir))
                    Directory.CreateDirectory(dataDir);

                Bootstrap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(Resources.Error_BootstrapError, ex.Message, ex.StackTrace), Resources.Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(0);
            }
        }

        private async void Bootstrap()
        {
            status.Text = Resources.Status_DownloadingLauncherData;
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

            Text += " - " + data.Version;

            if (!File.Exists(launcherPath))
                DownloadLauncher();
            else
            {
                if (Version.Parse(FileVersionInfo.GetVersionInfo(launcherPath).ProductVersion) >= currentVer)
                    RunLauncher();
                else DownloadLauncher();
            }
        }

        private void RunLauncher()
        {
            status.Text = Resources.Status_StartingLauncher;
            string launcherPath = Path.Combine(dataDir, "launcher.exe");
            Process.Start(launcherPath);
            Environment.Exit(0);
        }

        private void DownloadLauncher()
        {
            status.Text = Resources.Status_DownloadingLauncher;
            WebClient webClient = new WebClient();
            webClient.DownloadProgressChanged += launcherDownloadProgressChanged;
            webClient.DownloadFileCompleted += launcherDownloadCompleted;
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
            status.Text = Resources.Status_ValidatingLauncher;
            string launcherTempArchiveDir = Path.Combine(dataDir, "temp");
            string launcherTempArchive = Path.Combine(launcherTempArchiveDir, "launcher.zip");
            var hash = Sha256FileHash.CalculateFileHash(launcherTempArchive);
            var hashStr = Sha256FileHash.HashBytesToString(hash);
            if (hashStr != data.Hash)
            {
                MessageBox.Show(Resources.Error_LauncherVerification, Resources.Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                File.Delete(launcherTempArchive);
                Environment.Exit(0);
            }
            else
            {
                UnzipArchive();
            }
        }

        private void UnzipArchive()
        {
            status.Text = Resources.Status_ExtractingLauncher;
            string launcherTempArchiveDir = Path.Combine(dataDir, "temp");
            string launcherTempArchive = Path.Combine(launcherTempArchiveDir, "launcher.zip");
            ZipFile.ExtractToDirectory(launcherTempArchive, dataDir);
            RunLauncher();
        }

        private void launcherDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            status.Text = string.Format(Resources.Status_DownloadingLauncherProgress, e.ProgressPercentage, e.BytesReceived / 1024f / 1024f, e.TotalBytesToReceive / 1024f / 1024f);
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
            MessageBox.Show(string.Format(Resources.Error_BootstrapDataError, err.ErrorMessage, err.Error), Resources.Error_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
        }
    }
}
