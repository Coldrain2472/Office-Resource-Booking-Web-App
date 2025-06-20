namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class EndReservationResponse
    {
        public bool Success { get; set; }

        public string? ErrorMessage { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
