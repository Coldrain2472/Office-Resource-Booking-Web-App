using OfficeResourceBookingSystem.Services.DTOs.Reservation;

namespace OfficeResourceBookingSystem.Web.Models.Employee
{
    public class ReservationHistoryPerEmployeeViewModel
    {
        public string EmployeeName { get; set; }

        public List<ReservationInfo> Reservations { get; set; }

        public int TotalCount { get; set; }
    }
}
