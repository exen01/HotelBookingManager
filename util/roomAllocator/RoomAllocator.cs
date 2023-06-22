using HotelBookingManager.domain.dto;
using HotelBookingManager.service.booking;
using HotelBookingManager.service.room;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBookingManager.util.roomAllocator
{
    public class RoomAllocator
    {
        private readonly IRoomService roomService;
        private readonly IBookingService bookingService;

        private List<Room> availableRooms;
        private List<Booking> bookings;

        public RoomAllocator(IRoomService roomService, IBookingService bookingService)
        {
            this.bookingService = bookingService;
            this.roomService = roomService;
        }

        public void AllocateRooms()
        {
            bookings = bookingService.GetAllBookings();
            List<Booking> sortedBookings = bookings
                .Where(booking => booking.Status == 0)
                .OrderBy(booking => booking.ArrivalDate)
                .ToList();

            foreach (Booking booking in sortedBookings)
            {
                ProcessBooking(booking);
            }
        }

        private void ProcessBooking(Booking booking)
        {
            DateTime arrivalDate = booking.ArrivalDate;
            DateTime minimumArrivalDate = arrivalDate.AddDays(-3);

            availableRooms = roomService.GetRoomsByTypeId(booking.RoomTypeId);
            List<Room> filteredRooms = availableRooms
                .Where(room => room.Availability == 0)
                .ToList();

            if (filteredRooms.Count > 0 && minimumArrivalDate >= DateTime.Today)
            {
                booking.RoomId = filteredRooms[0].Id;
                booking.Status = 1;
                bookingService.UpdateBooking(booking);
            }
        }
    }
}
