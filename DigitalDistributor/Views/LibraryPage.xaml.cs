using System.Collections.Generic;
using System.Windows.Controls;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class LibraryPage : Page
    {
        public LibraryPage()
        {
            InitializeComponent();
            LoadLibrary();
        }

        private void LoadLibrary()
        {
            List<MediaContent> library = LibraryService.GetLibrary();

            if (library.Count == 0)
            {
                PanelEmpty.Visibility = System.Windows.Visibility.Visible;
                ScrollLib.Visibility  = System.Windows.Visibility.Collapsed;
                TxtCount.Text = "0 позицій";
            }
            else
            {
                PanelEmpty.Visibility = System.Windows.Visibility.Collapsed;
                ScrollLib.Visibility  = System.Windows.Visibility.Visible;
                LibraryItems.ItemsSource = library;
                TxtCount.Text = $"{library.Count} позицій у вашій бібліотеці";
            }
        }
    }
}
