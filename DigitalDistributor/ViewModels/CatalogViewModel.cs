using System.Collections.ObjectModel;
using DigitalDistributor.Models;

namespace DigitalDistributor.ViewModels
{
    public class CatalogViewModel
    {
        public ObservableCollection<MediaContent> ContentList { get; set; }

        public CatalogViewModel()
        {
            ContentList = new ObservableCollection<MediaContent>
            {
                new MediaContent { Title = "The Witcher 3: Wild Hunt", Genre = "RPG", Price = 599.99m, Type = ContentType.Game },
                new MediaContent { Title = "Cyberpunk 2077", Genre = "Action/RPG", Price = 899.00m, Type = ContentType.Game },
                new MediaContent { Title = "Adobe Photoshop", Genre = "Графіка", Price = 1200.50m, Type = ContentType.Software },
                new MediaContent { Title = "Interstellar", Genre = "Наукова фантастика", Price = 150.00m, Type = ContentType.Movie }
            };
        }
    }
}