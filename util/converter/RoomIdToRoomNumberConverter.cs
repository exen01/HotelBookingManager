using HotelBookingManager.domain.dto;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HotelBookingManager.util.converter
{
    internal class RoomIdToRoomNumberConverter : IValueConverter
    {
        private readonly List<Room> rooms;

        public RoomIdToRoomNumberConverter(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "Ожидает";
            }

            int roomId = (int)value;
            Room room = rooms.FirstOrDefault(c => c.Id == roomId);
            return room?.Number ?? 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
