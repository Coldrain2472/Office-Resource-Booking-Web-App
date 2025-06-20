namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class GetHistoryOfReservationsPerResourceResponse
    {
        public List<ReservationInfo> Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
