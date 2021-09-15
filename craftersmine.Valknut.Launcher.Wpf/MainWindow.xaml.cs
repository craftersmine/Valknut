using craftersmine.Valknut.Launcher.Authentication;
using craftersmine.Valknut.Launcher.Authentication.Models.Responses;
using craftersmine.Valknut.Launcher.Wpf.Properties;

using MaterialDesignThemes.Wpf;

using Swan.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace craftersmine.Valknut.Launcher.Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SnackbarMessageQueue SnackbarMessageQueue;

        public MainWindow()
        {
            InitializeComponent();
            SnackbarMessageQueue = new SnackbarMessageQueue();
            snackbar.MessageQueue = SnackbarMessageQueue;
            if (Settings.Default.RememberUser)
            {
                loginAnimation.Visibility = Visibility.Visible;
                emailBox.Text = Settings.Default.UserEmail;
                emailBox.IsEnabled = false;
                passwordBox.IsEnabled = false;
                registerButton.IsEnabled = false;
                loginButton.IsEnabled = false;
                rememberMe.IsEnabled = false;
                try
                {
                    var isValidated = Authenticator.ValidateUser(Settings.Default.AccessToken, Settings.Default.ClientToken).Result;
                    if (isValidated)
                    {
                        welcomeLabel.Text = string.Format(Properties.Resources.PlayFrame_WelcomeBox, Settings.Default.Username);
                        logoutMenu.IsEnabled = true;
                        settingsMenu.IsEnabled = true;
                        loginButton.SetValue(ButtonProgressAssist.ValueProperty, true);
                        SnackbarMessageQueue.Enqueue("Successfully logged in as " + Settings.Default.Username);
                        AnimateFramesSwitch(loginFrame, playFrame);
                    }

                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "Validation", "Unable to validate user access token!");
                    ShowMessage(Properties.Resources.Message_Error_Title, ex.Message);
                    emailBox.IsEnabled = true;
                    passwordBox.IsEnabled = true;
                    registerButton.IsEnabled = true;
                    loginButton.IsEnabled = true;
                    rememberMe.IsEnabled = true;
                    loginButton.SetValue(MaterialDesignThemes.Wpf.ButtonProgressAssist.IsIndeterminateProperty, false);
                }
                
            }
            settingsMenu.IsEnabled = false;
            logoutMenu.IsEnabled = false;
            rememberMe.IsChecked = Settings.Default.RememberUser;
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginAnimation.Visibility = Visibility.Visible;
            loginButton.SetValue(MaterialDesignThemes.Wpf.ButtonProgressAssist.IsIndeterminateProperty, true);
            emailBox.IsEnabled = false;
            passwordBox.IsEnabled = false;
            registerButton.IsEnabled = false;
            loginButton.IsEnabled = false;
            rememberMe.IsEnabled = false;
            try
            {
                Logger.Info("Authenticating...");

                bool isEmailValid = EmailValidationRule.Validate(emailBox.Text);
                if (!isEmailValid)
                {
                    loginAnimation.Visibility = Visibility.Collapsed;
                    emailBox.IsEnabled = true;
                    passwordBox.IsEnabled = true;
                    registerButton.IsEnabled = true;
                    loginButton.IsEnabled = true;
                    rememberMe.IsEnabled = true;
                    loginButton.SetValue(MaterialDesignThemes.Wpf.ButtonProgressAssist.IsIndeterminateProperty, false);
                    SnackbarMessageQueue.Enqueue(Properties.Resources.Validation_Email_FieldContentsIsNotEmail);
                    return;
                }

                var authenticationResponse = await Authenticator.Authenticate(emailBox.Text, passwordBox.Password);
                if (authenticationResponse is AuthenticationResponse)
                {
                    StaticData.AuthenticationResponse = (AuthenticationResponse)authenticationResponse;
                    Settings.Default.Username = StaticData.AuthenticationResponse.SelectedProfile.Name;
                    if (rememberMe.IsChecked == true)
                    {
                        Settings.Default.ClientToken = StaticData.AuthenticationResponse.ClientToken;
                        Settings.Default.Username = StaticData.AuthenticationResponse.SelectedProfile.Name;
                        Settings.Default.UserId = StaticData.AuthenticationResponse.SelectedProfile.Id;
                        Settings.Default.AccessToken = StaticData.AuthenticationResponse.AccessToken;
                        Settings.Default.UserEmail = emailBox.Text;
                        Settings.Default.RememberUser = (bool)rememberMe.IsChecked;
                    }
                    Settings.Default.Save();
                    loginAnimation.Visibility = Visibility.Collapsed;
                    welcomeLabel.Text = string.Format(Properties.Resources.PlayFrame_WelcomeBox, StaticData.AuthenticationResponse.SelectedProfile.Name);
                    logoutMenu.IsEnabled = true;
                    settingsMenu.IsEnabled = true;
                    AnimateFramesSwitch(loginFrame, playFrame);
                }
            }
            catch (Exception ex)
            {
                loginAnimation.Visibility = Visibility.Collapsed;
                Logger.Error(ex, "Authenticator", "Unable to authenticate user!");
                ShowMessage(Properties.Resources.Message_Error_Title, ex.Message);
                emailBox.IsEnabled = true;
                passwordBox.IsEnabled = true;
                registerButton.IsEnabled = true;
                loginButton.IsEnabled = true;
                rememberMe.IsEnabled = true;
                loginButton.SetValue(MaterialDesignThemes.Wpf.ButtonProgressAssist.IsIndeterminateProperty, false);
            }
        }

        private void ShowMessage(string title, string message)
        {
            dlgLabelContent.Text = message;
            dlgTitle.Text = title;
            aboutBox.Visibility = Visibility.Collapsed;
            settingsBox.Visibility = Visibility.Collapsed;
            messageBox.Visibility = Visibility.Visible;
            dialogHost.IsOpen = true;
        }

        private void AnimateFramesSwitch(Grid grid, Grid destGrid)
        {
            destGridInstance = destGrid;
            srcGridInstance = grid;
            DoubleAnimation fadeoutAnim = new DoubleAnimation();
            fadeoutAnim.From = grid.Opacity;
            fadeoutAnim.To = 0d;
            fadeoutAnim.Duration = new Duration(TimeSpan.FromSeconds(0.5d));
            fadeoutAnim.Completed += animationCompleted;
            grid.BeginAnimation(OpacityProperty, fadeoutAnim);
        }

        private Grid destGridInstance;
        private Grid srcGridInstance;

        private void animationCompleted(object sender, EventArgs e)
        {
            destGridInstance.Opacity = 0d;
            destGridInstance.Visibility = Visibility.Visible;
            srcGridInstance.Visibility = Visibility.Collapsed;
            srcGridInstance.Opacity = 1d;
            srcGridInstance = null;
            DoubleAnimation fadeinAnim = new DoubleAnimation();
            fadeinAnim.From = 0d;
            fadeinAnim.To = 1d;
            fadeinAnim.Duration = new Duration(TimeSpan.FromSeconds(0.5d));
            destGridInstance.BeginAnimation(OpacityProperty, fadeinAnim);
        }

        private void launchButton_Click(object sender, RoutedEventArgs e)
        {
            AnimateFramesSwitch(playFrame, initializingFrame);
        }

        private void settingsMenuClick(object sender, MouseButtonEventArgs e)
        {
            messageBox.Visibility = Visibility.Collapsed;
            aboutBox.Visibility = Visibility.Collapsed;
            settingsBox.Visibility = Visibility.Visible;
            settingsAllocatedMemLabel.Text = "";
            dialogHost.IsOpen = true;
        }

        private void aboutMenuClick(object sender, MouseButtonEventArgs e)
        {
            aboutTitleLabel.Text = LauncherSettings.LauncherTitle;
            aboutDevLabel.Text = string.Format(Properties.Resources.AboutBox_Label_Developer, LauncherSettings.LauncherDeveloper);
            aboutClientTokLabel.Text = string.Format(Properties.Resources.AboutBox_Label_ClientId, "Not available");
            if (StaticData.AuthenticationResponse != null)
                aboutClientTokLabel.Text = string.Format(Properties.Resources.AboutBox_Label_ClientId, StaticData.AuthenticationResponse.SelectedProfile.Id);
            settingsBox.Visibility = Visibility.Collapsed;
            messageBox.Visibility = Visibility.Collapsed;
            aboutBox.Visibility = Visibility.Visible;
            dialogHost.IsOpen = true;
        }

        private void dlgButtonOk_Click(object sender, RoutedEventArgs e)
        {
            dialogHost.IsOpen = false;
        }

        private void settingsApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: implement settings saving
        }

        private void settingsCancelButton_Click(object sender, RoutedEventArgs e)
        {
            dialogHost.IsOpen = false;
        }
    }
}
