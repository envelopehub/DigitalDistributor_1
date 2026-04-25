using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DigitalDistributor.Models;

namespace DigitalDistributor.Services
{
    // Простий сервіс бібліотеки — зберігаємо придбані id у файл
    public static class LibraryService
    {
        private static readonly string DataFolder = Path.Combine(
            System.AppDomain.CurrentDomain.BaseDirectory, "Data");

        private static readonly string FilePath = Path.Combine(DataFolder, "library.json");

        // Отримати список придбаного контенту поточного юзера
        public static List<MediaContent> GetLibrary()
        {
            if (!File.Exists(FilePath))
                return new List<MediaContent>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<MediaContent>>(json) ?? new List<MediaContent>();
        }

        // Додати контент до бібліотеки
        public static void AddToLibrary(MediaContent item)
        {
            var library = GetLibrary();

            // Не додаємо дублі
            if (library.Exists(x => x.Id == item.Id))
                return;

            library.Add(item);
            Save(library);
        }

        private static void Save(List<MediaContent> library)
        {
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(library, options));
        }
    }
}
