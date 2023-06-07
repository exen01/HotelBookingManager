using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace HotelBookingManager.util.statusConverter
{
    internal class StatusToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Получить значение статуса из исходного объекта
            int status = (int)value;

            // Вернуть соответствующий фон в зависимости от статуса
            switch (status)
            {
                case 0:
                    // Свободен
                    return new SolidColorBrush(Color.FromRgb(149, 213, 42));
                case 1:
                    // Забронирован
                    return new SolidColorBrush(Color.FromRgb(255, 216, 0));
                case 2:
                    // Занят
                    return new SolidColorBrush(Color.FromRgb(220, 20, 60));
                default:
                    // Другие значения статуса - фон по умолчанию
                    return new SolidColorBrush(Colors.Transparent);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
