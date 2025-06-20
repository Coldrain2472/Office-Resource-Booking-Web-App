namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class CreateReservationResponse : ReservationInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
