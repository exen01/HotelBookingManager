using HotelBookingManager.dao.booking;
using HotelBookingManager.dao.room;
using HotelBookingManager.domain.dto;
using System.Collections.Generic;

namespace HotelBookingManager.service.booking
{
    class BookingServiceImpl : IBookingService
    {
        private readonly IBookingDao bookingDao;
        private readonly IRoomDao roomDao;

        public BookingServiceImpl(IBookingDao bookingDao, IRoomDao roomDao)
        {
            this.bookingDao = bookingDao;
            this.roomDao = roomDao;
        }

        public void UpdateBooking(Booking booking)
        {
            ChangeRoomStatus(booking);
            bookingDao.UpdateBooking(booking);
        }

        public void AddBooking(Booking booking)
        {
            ChangeRoomStatus(booking);
            bookingDao.AddBooking(booking);
        }

        public void ChangeRoomStatus(Booking booking)
        {
            if (booking.RoomId.HasValue)
            {
                Room room = roomDao.GetRoomById(booking.RoomId.Value);

                if (booking.Status == 1 && booking.PaymentStatus == 1)
                {
                    room.Availability = 2;
                }
                else if (booking.Status == 1 && booking.PaymentStatus == 0)
                {
                    room.Availability = 1;
                }

                roomDao.UpdateRoom(room);
            }
        }

        public void DeleteBookingById(int id)
        {
            bookingDao.DeleteBookingById(id);
        }

        public List<Booking> GetAllBookings()
        {
            return bookingDao.GetAllBookings();
        }
    }
}
