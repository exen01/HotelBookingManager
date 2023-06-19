using HotelBookingManager.dao.booking;
using HotelBookingManager.domain.dto;
using System;
using System.Collections.Generic;

namespace HotelBookingManager.service.booking
{
    class BookingServiceImpl : IBookingService
    {
        private readonly IBookingDao bookingDao;

        public BookingServiceImpl(IBookingDao bookingDao)
        {
            this.bookingDao = bookingDao;
        }

        public void AddBooking(Booking booking)
        {
            bookingDao.AddBooking(booking);
        }

        public void DeleteBookingById(int id)
        {
            bookingDao.DeleteBookingById(id);
        }

        public List<Booking> GetAllBookings()
        {
            return bookingDao.GetAllBookings();
        }

        public void UpdateBooking(Booking booking)
        {
            bookingDao.UpdateBooking(booking);
        }
    }
}
