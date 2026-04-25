using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.Views
{
    public partial class CatalogPage : Page
    {
        // Повний список контенту (завантажений один раз)
        private List<MediaContent> _allContent = new List<MediaContent>();
        private string _currentFilter = "All";

        // Набір кольорів для карток
        private static readonly string[] CardColors =
        {
            "#0078D4", "#6B46C1", "#2F855A", "#D69E2E",
            "#C53030", "#2B6CB0", "#744210", "#285E61"
        };

        public CatalogPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            _allContent = ContentService.LoadContent();

            // Призначаємо кольори карткам по черзі
            for (int i = 0; i < _allContent.Count; i++)
            {
                if (string.IsNullOrEmpty(_allContent[i].ImageColor) || _allContent[i].ImageColor == "#2980B9")
                    _allContent[i].ImageColor = CardColors[i % CardColors.Length];
            }

            ApplyFilter();
        }

        // Застосовуємо поточний фільтр і пошук
        private void ApplyFilter()
        {
            string search = TxtSearch.Text.Trim().ToLower();

            var filtered = _allContent.Where(c =>
            {
                // Фільтр за типом
                bool typeMatch = _currentFilter == "All"
                    || c.Type.ToString() == _currentFilter;

                // Фільтр за пошуком
                bool searchMatch = string.IsNullOrEmpty(search)
                    || c.Title.ToLower().Contains(search)
                    || c.Genre.ToLower().Contains(search);

                return typeMatch && searchMatch;
            }).ToList();

            ContentItems.ItemsSource = filtered;
            TxtCount.Text = $"{filtered.Count} позицій";
        }

        // Пошук
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilter();
        }

        // Кнопки фільтрів
        private void BtnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                _currentFilter = btn.Tag?.ToString() ?? "All";

                // Скидаємо стиль у всіх кнопок
                BtnAll.Style      = (Style)FindResource("SidebarButton");
                BtnGames.Style    = (Style)FindResource("SidebarButton");
                BtnMovies.Style   = (Style)FindResource("SidebarButton");
                BtnSoftware.Style = (Style)FindResource("SidebarButton");

                // Активуємо натиснуту
                btn.Style = (Style)FindResource("SidebarButtonActive");

                ApplyFilter();
            }
        }

        // Перехід на деталі
        private void BtnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is MediaContent content)
            {
                NavigationService.Navigate(new ContentDetailsPage(content));
            }
        }
    }
}