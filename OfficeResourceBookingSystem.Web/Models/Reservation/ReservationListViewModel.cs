using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Web.Models.Reservation
{
    public class ActiveReservationsListViewModel
    {
        public List<ReservationViewModel> ActiveReservations { get; set; }

        public int TotalCount { get; set; }
    }

    public class InactiveReservationsListViewModel
    {
        public List<ReservationViewModel> InactiveReservations { get; set; }

        public int TotalCount { get; set; }
    }

    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        public string CreatedByName { get; set; }

        public int CreatedByEmployeeId { get; set; }

        public int ReservedResourceId { get; set; }

        public string ReservedResourceName { get; set; }

        [Required(ErrorMessage = "Purpose is required.")]
        public string Purpose { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Participants count must be at least 1")]
        public int ParticipantsCount { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Start time is required.")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "End time is required.")]
        public DateTime EndTime { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}