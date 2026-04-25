using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class ContentDetailsPage : Page
    {
        private MediaContent _content;

        public ContentDetailsPage(MediaContent content)
        {
            InitializeComponent();
            _content = content;

            // Прив'язуємо весь об'єкт як DataContext для Binding у XAML
            this.DataContext = content;

            // Заповнюємо текстові поля вручну (можна і через Binding)
            TxtTitle.Text       = content.Title;
            TxtGenre.Text       = content.Genre;
            TxtDescription.Text = string.IsNullOrWhiteSpace(content.Description)
                ? "Опис відсутній."
                : content.Description;
            TxtPrice.Text       = $"{content.Price:C}";
            TxtType.Text        = content.Type switch
            {
                ContentType.Game     => "🎮  Гра",
                ContentType.Movie    => "🎬  Фільм",
                ContentType.Software => "💻  Програма",
                _ => "📦  Інше"
            };
        }

        // Кнопка "Придбати"
        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            // Додаємо до бібліотеки поточного користувача
            LibraryService.AddToLibrary(_content);

            BtnBuy.Content    = "✓ Додано до бібліотеки";
            BtnBuy.IsEnabled  = false;

            MessageBox.Show($"'{_content.Title}' успішно додано до вашої бібліотеки!",
                "Покупка", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}
