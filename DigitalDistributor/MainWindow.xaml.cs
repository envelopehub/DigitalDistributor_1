using System.Windows;
using System.Windows.Controls;
using DigitalDistributor.Models;
using DigitalDistributor.Services;
using DigitalDistributor.Views;

namespace DigitalDistributor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupUserInfo();
            MainFrame.Navigate(new CatalogPage());
        }

        private void SetupUserInfo()
        {
            var user = AuthService.CurrentUser;
            if (user == null) return;

            TxtUserName.Text    = user.Login;
            TxtUserRole.Text    = user.Role == UserRole.Admin ? "Адміністратор" : "Користувач";
            TxtUserInitial.Text = user.Login.Length > 0
                ? user.Login[0].ToString().ToUpper()
                : "?";

            if (user.Role == UserRole.Admin)
                BtnAdmin.Visibility = Visibility.Visible;
        }

        // Скидаємо всі кнопки до стандартного стилю, потім активуємо обрану
        private void SetActive(Button active)
        {
            BtnCatalog.Style  = (Style)FindResource("SidebarButton");
            BtnLibrary.Style  = (Style)FindResource("SidebarButton");
            BtnSettings.Style = (Style)FindResource("SidebarButton");
            BtnAbout.Style    = (Style)FindResource("SidebarButton");
            BtnAdmin.Style    = (Style)FindResource("SidebarButton");

            active.Style = (Style)FindResource("SidebarButtonActive");
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e)
        {
            SetActive(BtnCatalog);
            MainFrame.Navigate(new CatalogPage());
        }

        private void BtnLibrary_Click(object sender, RoutedEventArgs e)
        {
            SetActive(BtnLibrary);
            MainFrame.Navigate(new LibraryPage());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            SetActive(BtnSettings);
            MainFrame.Navigate(new SettingsPage());
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            SetActive(BtnAbout);
            MainFrame.Navigate(new AboutPage());
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            SetActive(BtnAdmin);
            MainFrame.Navigate(new AdminPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AuthService.Logout();
            var auth = new AuthWindow();
            auth.Show();
            this.Close();
        }
    }
}