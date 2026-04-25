using System.Collections.ObjectModel;
using DigitalDistributor.Models;
using DigitalDistributor.Services;

namespace DigitalDistributor.ViewModels
{
    // ViewModel для каталогу — завантажує дані з ContentService
    public class CatalogViewModel
    {
        public ObservableCollection<MediaContent> ContentList { get; set; }

        public CatalogViewModel()
        {
            var data = ContentService.LoadContent();
            ContentList = new ObservableCollection<MediaContent>(data);
        }
    }
}