namespace HotelBookingManager.domain.dto
{
    public class Room
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int TypeId { get; set; }
        public decimal Cost { get; set; }
        public int Availability { get; set; }
        public string Description { get; set; }
    }
}
