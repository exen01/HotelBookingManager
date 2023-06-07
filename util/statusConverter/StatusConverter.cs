using System;
using System.Globalization;
using System.Windows.Data;

namespace HotelBookingManager.util.statusConverter
{
    internal class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            string statusString = string.Empty;

            switch (status)
            {
                case 0:
                    statusString = "Свободен";
                    break;
                case 1:
                    statusString = "Забронирован";
                    break;
                case 2:
                    statusString = "Проживание";
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
