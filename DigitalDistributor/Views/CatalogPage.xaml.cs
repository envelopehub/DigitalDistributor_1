using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DigitalDistributor.Views
{
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
        }

        private void BtnItem_Click(object sender, RoutedEventArgs e)
        {
            // Отримуємо кнопку, на яку натиснули
            Button clickedButton = (Button)sender;

            // Отримуємо назву товару (текст з кнопки)
            string itemName = clickedButton.Content.ToString() ?? "Невідомий товар";

            // Виконуємо перехід, передаючи назву в конструктор ContentDetailsPage
            NavigationService.Navigate(new ContentDetailsPage(itemName));
        }
    }
}