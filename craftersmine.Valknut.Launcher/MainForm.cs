using craftersmine.Valknut.Launcher.Authentication;
using craftersmine.Valknut.Launcher.Authentication.Models;
using craftersmine.Valknut.Launcher.Authentication.Models.Responses;

using MaterialSkin.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace craftersmine.Valknut.Launcher
{
    public partial class MainForm : MaterialForm
    {
        public MainForm()
        {
            InitializeComponent();
            MaterialSkin.MaterialSkinManager.Instance.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Green500, MaterialSkin.Primary.Green700, MaterialSkin.Primary.Green300, MaterialSkin.Accent.Green400, MaterialSkin.TextShade.BLACK);
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            loginForm.Enabled = false;
            waitAnim.Visible = true;
            var response = await Authenticator.Authenticate(emailBox.Text, passwordBox.Text);
            AuthenticationResponse authenticationResponse = null;
            if (response is AuthenticationResponse)
            {
                authenticationResponse = (AuthenticationResponse)response;
                MessageBox.Show(authenticationResponse.SelectedProfile.Name + " : " + authenticationResponse.SelectedProfile.Id);
            }
            else
            {
                var errResp = (ErrorResponse)response;
                MessageBox.Show(errResp.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            waitAnim.Visible = false;
            loginForm.Enabled = true;
        }
    }
}
