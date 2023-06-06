using HotelBookingManager.dao.room;
using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.room
{
    class RoomServiceImpl : IRoomService
    {
        private readonly IRoomDao roomDao;

        public RoomServiceImpl(IRoomDao roomDao)
        {
            this.roomDao = roomDao;
        }

        public void AddRoom(Room room)
        {
            roomDao.AddRoom(room);
        }

        public void DeleteRoomById(int id)
        {
            roomDao.DeleteRoomById(id);
        }

        public List<Room> GetAllRooms()
        {
            return roomDao.GetAllRooms();
        }

        public Room GetRoomById(int id)
        {
            var result = roomDao.GetRoomById(id);
            if (result == null)
            {
                result = new Room();
            }

            return result;
        }

        public void UpdateRoom(Room room)
        {
            roomDao.UpdateRoom(room);
        }
    }
}
