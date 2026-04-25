using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class AdminPage : Page
    {
        // Список контенту — відображається у таблиці
        private ObservableCollection<MediaContent> _contentList = new ObservableCollection<MediaContent>();
        // Зараз вибраний (для редагування)
        private MediaContent? _selectedContent;

        public AdminPage()
        {
            InitializeComponent();
            LoadContent();
            LoadUsers();
        }

        // Завантажуємо контент з файлу
        private void LoadContent()
        {
            var data = ContentService.LoadContent();
            _contentList = new ObservableCollection<MediaContent>(data);
            ContentDataGrid.ItemsSource = _contentList;
        }

        // Завантажуємо користувачів з файлу
        private void LoadUsers()
        {
            List<User> users = AuthService.LoadUsers();
            UsersDataGrid.ItemsSource = users;
            TxtUserCount.Text = $"Всього: {users.Count}";
        }

        // Кнопка оновлення списку юзерів
        private void BtnRefreshUsers_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
        }

        // Вибір рядка в таблиці — заповнюємо форму
        private void ContentGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentDataGrid.SelectedItem is MediaContent selected)
            {
                _selectedContent = selected;

                TxtTitle.Text        = selected.Title;
                TxtGenre.Text        = selected.Genre;
                TxtDesc.Text         = selected.Description;
                TxtPrice.Text        = selected.Price.ToString();
                CmbType.SelectedIndex = (int)selected.Type;

                HideError();
            }
        }

        // Зберегти / Додати
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // --- Валідація ---
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                ShowError("Назва контенту є обов'язковою!");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtGenre.Text))
            {
                ShowError("Жанр є обов'язковим полем.");
                return;
            }
            if (!decimal.TryParse(TxtPrice.Text.Replace(".", ","), out decimal price) || price < 0)
            {
                ShowError("Ціна повинна бути числом більшим або рівним 0.");
                return;
            }
            if (CmbType.SelectedIndex == -1)
            {
                ShowError("Оберіть тип контенту.");
                return;
            }

            HideError();

            ContentType selectedType = (ContentType)CmbType.SelectedIndex;

            if (_selectedContent == null)
            {
                // --- CREATE ---
                var newItem = new MediaContent
                {
                    Title       = TxtTitle.Text.Trim(),
                    Genre       = TxtGenre.Text.Trim(),
                    Description = TxtDesc.Text.Trim(),
                    Price       = price,
                    Type        = selectedType
                };
                _contentList.Add(newItem);
                MessageBox.Show("Новий контент успішно додано!", "Успіх",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // --- UPDATE ---
                _selectedContent.Title       = TxtTitle.Text.Trim();
                _selectedContent.Genre       = TxtGenre.Text.Trim();
                _selectedContent.Description = TxtDesc.Text.Trim();
                _selectedContent.Price       = price;
                _selectedContent.Type        = selectedType;

                ContentDataGrid.Items.Refresh();
                MessageBox.Show("Запис успішно оновлено!", "Успіх",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Зберігаємо у файл
            ContentService.SaveContent(new List<MediaContent>(_contentList));
            ClearForm();
        }

        // Видалити
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedContent == null)
            {
                ShowError("Спочатку виберіть елемент у таблиці.");
                return;
            }

            var result = MessageBox.Show(
                $"Видалити '{_selectedContent.Title}'?",
                "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                _contentList.Remove(_selectedContent);
                ContentService.SaveContent(new List<MediaContent>(_contentList));
                ClearForm();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedContent      = null;
            TxtTitle.Text         = string.Empty;
            TxtGenre.Text         = string.Empty;
            TxtDesc.Text          = string.Empty;
            TxtPrice.Text         = string.Empty;
            CmbType.SelectedIndex = -1;
            ContentDataGrid.SelectedItem = null;
            HideError();
        }

        private void ShowError(string msg)
        {
            TxtError.Text       = $"⚠ {msg}";
            TxtError.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            TxtError.Visibility = Visibility.Collapsed;
        }
    }
}