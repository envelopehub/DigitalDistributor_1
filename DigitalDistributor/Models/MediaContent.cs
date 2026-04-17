using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DigitalDistributor.Models
{
    public enum ContentType { Game, Movie, Software }

    // Реалізуємо INotifyPropertyChanged для оновлення UI в реальному часі
    public class MediaContent : INotifyPropertyChanged
    {
        private Guid _id = Guid.NewGuid();
        private string _title = string.Empty;
        private string _genre = string.Empty;
        private decimal _price;
        private ContentType _type;

        public Guid Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Genre
        {
            get => _genre;
            set { _genre = value; OnPropertyChanged(); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(); }
        }

        public ContentType Type
        {
            get => _type;
            set { _type = value; OnPropertyChanged(); }
        }

        // Подія, яка сповіщає XAML про зміну значення
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}