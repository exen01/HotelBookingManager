namespace HotelBookingManager.domain.dto
{
    internal class Room
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }
        public int Availability { get; set; }
        public string Description { get; set; }
    }
}
