using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.roomType
{
    public interface IRoomTypeService
    {
        RoomType GetRoomTypeById(int id);
        List<RoomType> GetAllRoomTypes();
    }
}
