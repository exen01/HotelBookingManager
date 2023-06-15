using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HotelBookingManager.util.statusConverter
{
    internal class BookingPaymentStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int status = (int)value;
            string statusString = string.Empty;

            switch (status)
            {
                case 0:
                    statusString = "Ожидает оплаты";
                    break;
                case 1:
                    statusString = "Оплачено";
                    break;
                case 2:
                    statusString = "Ошибка оплаты";
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
