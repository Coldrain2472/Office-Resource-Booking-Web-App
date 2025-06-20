namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class EndReservationRequest
    {
        public int ReservationId { get; set; }

        public int CreatedByEmployeeId { get; set; }
    }
}
