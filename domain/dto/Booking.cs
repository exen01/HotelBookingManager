using System;

namespace HotelBookingManager.domain.dto
{
    internal class Booking
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int RoomId { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set;}
        public int DurationOfStay { get; set; }
        public string AdditionalInformation { get; set; }
        public string Status { get; set; }
        public decimal TotalCost { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
