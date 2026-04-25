using System;
using System.Windows;
using System.Windows.Controls;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        // Кнопка Увійти
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login    = TxtLoginLogin.Text.Trim();
            string password = TxtLoginPassword.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ShowLoginError("Заповніть усі поля!");
                return;
            }

            if (AuthService.Login(login, password))
            {
                // Відкриваємо головне вікно і закриваємо авторизацію
                var main = new MainWindow();
                main.Show();
                this.Close();
            }
            else
            {
                ShowLoginError("Невірний логін або пароль!");
            }
        }

        // Кнопка Зареєструватися
        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string login    = TxtRegLogin.Text.Trim();
            string password = TxtRegPassword.Password;

            // Валідація
            if (string.IsNullOrWhiteSpace(login))
            {
                ShowRegError("Введіть логін!");
                return;
            }
            if (login.Length < 3)
            {
                ShowRegError("Логін має бути не менше 3 символів.");
                return;
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ShowRegError("Введіть пароль!");
                return;
            }
            if (password.Length < 6)
            {
                ShowRegError("Пароль має бути не менше 6 символів.");
                return;
            }

            // Визначаємо роль за вибором ComboBox
            UserRole role = UserRole.User;
            if (CmbRole.SelectedItem is ComboBoxItem item && item.Tag?.ToString() == "Admin")
                role = UserRole.Admin;

            if (AuthService.Register(login, password, role))
            {
                MessageBox.Show("Реєстрація успішна! Тепер увійдіть.",
                    "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                // Переходимо до форми входу
                TxtRegLogin.Text   = string.Empty;
                TxtRegPassword.Password = string.Empty;
                ShowLoginPanel();
            }
            else
            {
                ShowRegError("Користувач з таким логіном вже існує!");
            }
        }

        // Перемикання на форму реєстрації
        private void BtnShowRegister_Click(object sender, RoutedEventArgs e)
        {
            PanelLogin.Visibility    = Visibility.Collapsed;
            PanelRegister.Visibility = Visibility.Visible;
            TxtRegError.Visibility   = Visibility.Collapsed;
        }

        // Перемикання на форму входу
        private void BtnShowLogin_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            PanelRegister.Visibility = Visibility.Collapsed;
            PanelLogin.Visibility    = Visibility.Visible;
            TxtLoginError.Visibility = Visibility.Collapsed;
        }

        private void ShowLoginError(string msg)
        {
            TxtLoginError.Text       = msg;
            TxtLoginError.Visibility = Visibility.Visible;
        }

        private void ShowRegError(string msg)
        {
            TxtRegError.Text       = msg;
            TxtRegError.Visibility = Visibility.Visible;
        }

        // Перемикання мови на льоту
        private void CmbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbLanguage.SelectedItem is ComboBoxItem item && item.Tag != null)
            {
                string culture = item.Tag.ToString()!;
                var dict = new ResourceDictionary
                {
                    Source = new Uri($"Resources/Lang.{culture}.xaml", UriKind.Relative)
                };

                // Індекс 0 — Тема, індекс 1 — Styles.xaml, індекс 2 — мовний файл
                var merged = Application.Current.Resources.MergedDictionaries;
                if (merged.Count > 2)
                    merged.RemoveAt(2);
                merged.Add(dict);
            }
        }
    }
}