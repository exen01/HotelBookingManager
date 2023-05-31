using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.dao.room
{
    internal interface IRoomDao
    {
        void AddRoom(Room room);
        void DeleteRoomById(int id);
        void UpdateRoom(Room room);
        List<Room> GetAllRooms();
        Room GetRoomById(int id);
        List<Room> GetRoomsByType(string type);
    }
}
