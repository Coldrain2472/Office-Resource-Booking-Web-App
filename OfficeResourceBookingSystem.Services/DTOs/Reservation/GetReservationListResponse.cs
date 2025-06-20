namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class GetReservationListResponse
    {
        public List<ReservationInfo>? Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
