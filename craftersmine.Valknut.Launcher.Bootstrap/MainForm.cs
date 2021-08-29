using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.Valknut.Launcher.Bootstrap
{
    public partial class MainForm : Form
    {
        string address = "";
        public MainForm()
        {
            InitializeComponent();

            status.Text = "Loading Valknut Bootstrapper...";
            if (BootstrapSettings.ServerPort != 80)
                address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + ":" + BootstrapSettings.ServerPort + "/valknut/";
            else address = BootstrapSettings.ServerProtocol + "://" + BootstrapSettings.ServerAddress + "/valknut/";

            Bootstrap();
        }

        private async void Bootstrap()
        {
            status.Text = "Downloading launcher data...";
            var resp = await HttpHelper.MakeGetRequest(address + "getBootstrapData", null);
            BootstrapDataResponse data;
            if (resp.IsSuccessful)
                data = (BootstrapDataResponse)resp;
            else Fail((ErrorResponse)resp);
        }

        private void Fail(ErrorResponse error)
        {
            MessageBox.Show("Something went wrong during launcher bootstrapping!\r\nPlease try again or contact support!\r\n\r\n" + error.ErrorMessage + "\r\nStatus code: " + error.Error);
            Environment.Exit(0);
        }
    }
}
