using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using DigitalDistributor.Models;

namespace DigitalDistributor.Views
{
    public partial class AdminPage : Page
    {
        // Наша база даних у пам'яті (поки не підключили JSON)
        private ObservableCollection<MediaContent> _contentList;
        // Змінна для збереження того, що ми зараз редагуємо
        private MediaContent _selectedContent;

        public AdminPage()
        {
            InitializeComponent();

            // Ініціалізація списку
            _contentList = new ObservableCollection<MediaContent>
            {
                new MediaContent { Title = "The Witcher 3", Genre = "RPG", Price = 599.99m, Type = ContentType.Game }
            };

            // Прив'язуємо таблицю до нашого списку (Read)
            ContentDataGrid.ItemsSource = _contentList;
        }

        // Обробник натискання "Зберегти" (Create та Update)
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // --- ВАЛІДАЦІЯ ДАНИХ ---
            if (string.IsNullOrWhiteSpace(TxtTitle.Text))
            {
                ShowError("Назва контенту є обов'язковою!");
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtGenre.Text))
            {
                ShowError("Поле жанру не може бути порожнім.");
                return;
            }
            // Перевірка, чи ціна є числом і чи вона більша за 0
            if (!decimal.TryParse(TxtPrice.Text, out decimal price) || price < 0)
            {
                ShowError("Ціна повинна бути коректним додатнім числом (наприклад, 150,50).");
                return;
            }
            if (CmbType.SelectedIndex == -1)
            {
                ShowError("Будь ласка, оберіть тип контенту.");
                return;
            }

            // Якщо валідація пройдена, ховаємо помилки
            HideError();

            // --- БІЗНЕС ЛОГІКА: СТВОРЕННЯ (Create) ---
            if (_selectedContent == null)
            {
                var newItem = new MediaContent
                {
                    Title = TxtTitle.Text,
                    Genre = TxtGenre.Text,
                    Price = price,
                    Type = (ContentType)CmbType.SelectedIndex // Індекси: 0-Game, 1-Movie, 2-Software
                };
                _contentList.Add(newItem);
                MessageBox.Show("Новий контент успішно додано до бази!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            // --- БІЗНЕС ЛОГІКА: ОНОВЛЕННЯ (Update) ---
            else
            {
                _selectedContent.Title = TxtTitle.Text;
                _selectedContent.Genre = TxtGenre.Text;
                _selectedContent.Price = price;
                _selectedContent.Type = (ContentType)CmbType.SelectedIndex;

                ContentDataGrid.Items.Refresh(); // Оновлюємо таблицю
                MessageBox.Show("Запис успішно оновлено!", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ClearForm();
        }

        // Обробник натискання "Видалити" (Delete)
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedContent != null)
            {
                MessageBoxResult result = MessageBox.Show($"Ви впевнені, що хочете видалити '{_selectedContent.Title}'?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    _contentList.Remove(_selectedContent);
                    ClearForm();
                }
            }
            else
            {
                ShowError("Спочатку виберіть елемент у таблиці для видалення.");
            }
        }

        // Коли вибираємо рядок у таблиці, дані переносяться у форму
        private void ContentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentDataGrid.SelectedItem is MediaContent selected)
            {
                _selectedContent = selected; // Зберігаємо посилання на об'єкт

                // Заповнюємо форму
                TxtTitle.Text = selected.Title;
                TxtGenre.Text = selected.Genre;
                TxtPrice.Text = selected.Price.ToString();
                CmbType.SelectedIndex = (int)selected.Type;

                HideError();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        // Допоміжні методи
        private void ClearForm()
        {
            _selectedContent = null;
            TxtTitle.Text = string.Empty;
            TxtGenre.Text = string.Empty;
            TxtPrice.Text = string.Empty;
            CmbType.SelectedIndex = -1;
            ContentDataGrid.SelectedItem = null;
            HideError();
        }

        private void ShowError(string message)
        {
            TxtError.Text = $"Помилка: {message}";
            TxtError.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            TxtError.Visibility = Visibility.Collapsed;
        }
    }
}