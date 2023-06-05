namespace HotelBookingManager.domain.constatnt
{
    class RegExp
    {
        public static readonly string clientName = "^[\\p{L}\\s'-.]+$";
        public static readonly string clientAddress = "^[\\p{L}\\d\\s.,'-]+$";
    }
}
