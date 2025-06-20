namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class GetHistoryOfReservationsPerEmployeeResponse
    {
        public List<ReservationInfo> Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
