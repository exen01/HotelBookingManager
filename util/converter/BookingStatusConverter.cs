using System;
using System.Globalization;
using System.Windows.Data;

namespace HotelBookingManager.util.statusConverter
{
    internal class BookingStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            string statusString = string.Empty;

            switch (status)
            {
                case 0:
                    statusString = "Ожидает подтверждения";
                    break;
                case 1:
                    statusString = "Подтверждено";
                    break;
                case 2:
                    statusString = "Отменено";
                    break;
            }

            return statusString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
