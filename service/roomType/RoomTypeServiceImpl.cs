using HotelBookingManager.dao.roomType;
using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.roomType
{
    public class RoomTypeServiceImpl : IRoomTypeService
    {
        private readonly IRoomTypeDao roomTypeDao;

        public RoomTypeServiceImpl(IRoomTypeDao roomTypeDao)
        {
            this.roomTypeDao = roomTypeDao;
        }

        public List<RoomType> GetAllRoomTypes()
        {
            return roomTypeDao.GetAllRoomTypes();
        }

        public RoomType GetRoomTypeById(int id)
        {
            var result = roomTypeDao.GetRoomTypeById(id);
            if (result == null)
            {
                return new RoomType();
            }

            return result;
        }
    }
}
