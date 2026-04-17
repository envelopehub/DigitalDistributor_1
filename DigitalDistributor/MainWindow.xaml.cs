using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalDistributor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCatalog_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loading Catalog...");
            // На наступних етапах тут буде: MainFrame.Navigate(new CatalogPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Закриття програми 
        }
        // Додай ці методи поруч із BtnCatalog_Click
        private void BtnLibrary_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loading My Library...");
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            // Пізніше тут буде перевірка на роль Адміна
            MessageBox.Show("Loading Admin Panel...");
        }
    }

}