using System;
using System.Windows;
using System.Windows.Controls;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            var user = AuthService.CurrentUser;
            if (user == null) return;

            TxtLoginName.Text = user.Login;
            TxtRoleName.Text  = user.Role == UserRole.Admin ? "Адміністратор" : "Користувач";
            TxtInitial.Text   = user.Login.Length > 0
                ? user.Login[0].ToString().ToUpper()
                : "?";
        }

        // Перемикання теми
        // Тема — це індекс 0 у MergedDictionaries
        private void BtnTheme_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;

            string themeName = btn.Tag?.ToString() ?? "Light";
            string path = $"Resources/Themes/Theme.{themeName}.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri(path, UriKind.Relative)
            };

            // Замінюємо словник теми (індекс 0)
            Application.Current.Resources.MergedDictionaries[0] = dict;
        }

        // Перемикання мови (індекс 2)
        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbLanguage.SelectedItem is not ComboBoxItem item) return;
            if (item.Tag == null) return;

            string culture = item.Tag.ToString()!;
            var dict = new ResourceDictionary
            {
                Source = new Uri($"Resources/Lang.{culture}.xaml", UriKind.Relative)
            };

            var merged = Application.Current.Resources.MergedDictionaries;
            if (merged.Count > 2)
                merged.RemoveAt(2);
            merged.Add(dict);
        }
    }
}
