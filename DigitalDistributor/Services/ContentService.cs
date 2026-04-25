using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DigitalDistributor.Models;

namespace DigitalDistributor.Services
{
    // Сервіс для роботи з каталогом контенту (зберігання у файл)
    public static class ContentService
    {
        private static readonly string DataFolder = Path.Combine(
            System.AppDomain.CurrentDomain.BaseDirectory, "Data");

        private static readonly string FilePath = Path.Combine(DataFolder, "content.json");

        // Завантажуємо контент з файлу. Якщо файл відсутній — повертаємо приклади
        public static List<MediaContent> LoadContent()
        {
            if (!File.Exists(FilePath))
                return GetSampleContent();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<MediaContent>>(json) ?? GetSampleContent();
        }

        // Зберігаємо список контенту у файл
        public static void SaveContent(List<MediaContent> items)
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(items, options));
        }

        // Стартовий набір для демонстрації
        private static List<MediaContent> GetSampleContent()
        {
            return new List<MediaContent>
            {
                new MediaContent { Title = "The Witcher 3: Wild Hunt", Genre = "RPG", Price = 599.99m, Type = ContentType.Game, Description = "Відкритий світ, глибокий сюжет та незабутні персонажі." },
                new MediaContent { Title = "Cyberpunk 2077", Genre = "Action/RPG", Price = 899.00m, Type = ContentType.Game, Description = "Майбутнє мегаполісу Night City у твоїх руках." },
                new MediaContent { Title = "Interstellar", Genre = "Наукова фантастика", Price = 150.00m, Type = ContentType.Movie, Description = "Епічна одіссея крізь простір і час від Крістофера Нолана." },
                new MediaContent { Title = "Adobe Photoshop", Genre = "Графіка", Price = 1200.50m, Type = ContentType.Software, Description = "Професійний редактор зображень від Adobe." },
                new MediaContent { Title = "Dune: Part Two", Genre = "Фантастика", Price = 180.00m, Type = ContentType.Movie, Description = "Продовження захопливої саги про планету Арракіс." },
                new MediaContent { Title = "Hollow Knight", Genre = "Metroidvania", Price = 249.00m, Type = ContentType.Game, Description = "Дослідження темного підземного королівства комах." }
            };
        }
    }
}
