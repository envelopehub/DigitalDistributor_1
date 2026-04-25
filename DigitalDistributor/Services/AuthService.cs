using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using DigitalDistributor.Models;
using BCrypt.Net;

namespace DigitalDistributor.Services
{
    // Сервіс для роботи з користувачами: реєстрація, вхід, завантаження списку
    public static class AuthService
    {
        // Папка Data поряд з .exe файлом
        private static readonly string DataFolder = Path.Combine(
            System.AppDomain.CurrentDomain.BaseDirectory, "Data");

        private static readonly string FilePath = Path.Combine(DataFolder, "users.json");

        // Поточний залогінений користувач
        public static User? CurrentUser { get; private set; }

        // Завантажуємо список користувачів з файлу
        public static List<User> LoadUsers()
        {
            if (!File.Exists(FilePath))
                return new List<User>();

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
        }

        // Зберігаємо список у файл
        private static void SaveUsers(List<User> users)
        {
            // Створюємо папку, якщо її ще немає
            if (!Directory.Exists(DataFolder))
                Directory.CreateDirectory(DataFolder);

            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(FilePath, JsonSerializer.Serialize(users, options));
        }

        // Реєстрація нового користувача
        public static bool Register(string login, string password, UserRole role = UserRole.User)
        {
            var users = LoadUsers();

            // Перевіряємо чи логін вже зайнятий
            if (users.Any(u => u.Login == login))
                return false;

            // Хешуємо пароль за допомогою BCrypt (безпечно!)
            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            users.Add(new User
            {
                Login    = login,
                PasswordHash = hash,
                Role     = role
            });

            SaveUsers(users);
            return true;
        }

        // Вхід у систему
        public static bool Login(string login, string password)
        {
            var user = LoadUsers().FirstOrDefault(u => u.Login == login);

            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                CurrentUser = user;
                return true;
            }

            return false;
        }

        // Вихід
        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}