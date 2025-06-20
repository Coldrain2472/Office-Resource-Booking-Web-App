namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class UpdateReservationResponse : ReservationInfo
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
