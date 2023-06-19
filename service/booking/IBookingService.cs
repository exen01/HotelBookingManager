using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.booking
{
    interface IBookingService
    {
        List<Booking> GetAllBookings();
        void DeleteBookingById(int id);
        void UpdateBooking(Booking booking);
        void AddBooking(Booking booking);
    }
}
