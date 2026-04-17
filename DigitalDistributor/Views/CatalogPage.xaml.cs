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
            Button clickedButton = (Button)sender;

            string itemName = clickedButton.Content.ToString() ?? "Невідомий товар";

            NavigationService.Navigate(new ContentDetailsPage(itemName));
        }
    }
}