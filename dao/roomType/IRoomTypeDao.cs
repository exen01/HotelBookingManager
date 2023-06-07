using HotelBookingManager.domain.dto;

namespace HotelBookingManager.dao.roomType
{
    public interface IRoomTypeDao
    {
        RoomType? GetRoomTypeById(int id);
    }
}
