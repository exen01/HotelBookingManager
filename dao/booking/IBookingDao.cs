using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.dao.booking
{
    internal interface IBookingDao
    {
        void AddBooking(Booking booking);
        void DeleteBookingById(int id);
        void UpdateBooking(Booking booking);
        List<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        List<Booking> GetBookingsByClientId(int clientId);
        List<Booking> GetBookingsByRoomId(int roomId);
        List<Booking> GetBookingsByStatus(string status);
    }
}
