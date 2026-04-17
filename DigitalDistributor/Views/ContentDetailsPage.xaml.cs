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
using DigitalDistributor.Views;

namespace DigitalDistributor.Views
{
    /// <summary>
    /// Interaction logic for ContentDetailsPage.xaml
    /// </summary>
    public partial class ContentDetailsPage : Page
    {
        public ContentDetailsPage(string contentName)
        {
            InitializeComponent();

            // Встановлюємо переданий текст у наш TextBlock
            TxtTitle.Text = $"Деталі: {contentName}";
        }

        // Обробник кнопки "Назад"
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи є куди повертатися в історії Frame
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
