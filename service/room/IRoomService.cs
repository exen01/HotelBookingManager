using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.room
{
    public interface IRoomService
    {
        void AddRoom(Room room);
        void DeleteRoomById(int id);
        void UpdateRoom(Room room);
        List<Room> GetAllRooms();
        List<Room> GetRoomsByTypeId(int typeId);
        Room GetRoomById(int id);
        bool IsRoomNumberUnique(int number);
    }
}
