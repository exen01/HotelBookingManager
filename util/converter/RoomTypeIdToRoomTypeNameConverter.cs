using HotelBookingManager.domain.dto;
using HotelBookingManager.service.roomType;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HotelBookingManager.util.converter
{
    internal class RoomTypeIdToRoomTypeNameConverter : IValueConverter
    {
        private readonly IRoomTypeService roomTypeService;

        public RoomTypeIdToRoomTypeNameConverter(IRoomTypeService roomTypeService)
        {
            this.roomTypeService = roomTypeService;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<RoomType> roomTypes = roomTypeService.GetAllRoomTypes();

            int roomTypeId = (int)value;
            RoomType roomType = roomTypes.FirstOrDefault(c => c.Id == roomTypeId);

            return roomType?.Name ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
