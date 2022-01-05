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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace craftersmine.Valknut.Launcher.Wpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public bool? IsRememberMeChecked { get { return rememberMe.IsChecked; } set { rememberMe.IsChecked = value; } }
        public bool ShowAnimation { get { return loginAnimation.Visibility == Visibility.Visible; } set { if (value) loginAnimation.Visibility = Visibility.Visible; else loginAnimation.Visibility = Visibility.Hidden; } }

        public event EventHandler<RoutedEventArgs> LoginClicked;
        public event EventHandler<RoutedEventArgs> RegisterClicked;

        public LoginPage()
        {
            InitializeComponent();
        }

        private void loginClicked(object sender, RoutedEventArgs args)
        {
            LoginClicked?.Invoke(this, args);
        }

        private void registerClicked(object sender, RoutedEventArgs args)
        {
            RegisterClicked?.Invoke(this, args);
        }
    }
}
