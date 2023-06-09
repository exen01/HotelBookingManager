﻿using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.dao.roomType
{
    public interface IRoomTypeDao
    {
        RoomType? GetRoomTypeById(int id);
        List<RoomType> GetAllRoomTypes();
    }
}
