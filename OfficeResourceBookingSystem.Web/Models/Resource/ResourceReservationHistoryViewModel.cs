using OfficeResourceBookingSystem.Services.DTOs.Reservation;

namespace OfficeResourceBookingSystem.Web.Models.Resource
{
    public class ResourceReservationHistoryViewModel
    {
        public string ResourceName { get; set; }

        public List<ReservationInfo> Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
