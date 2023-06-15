using HotelBookingManager.domain.dto;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HotelBookingManager.util.converter
{
    internal class ClientIdToClientNameConverter : IValueConverter
    {
        private readonly List<Client> clients;

        public ClientIdToClientNameConverter(List<Client> clients)
        {
            this.clients = clients;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int clientId = (int)value;
            Client client = clients.FirstOrDefault(c => c.Id == clientId);
            return client?.Name ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
