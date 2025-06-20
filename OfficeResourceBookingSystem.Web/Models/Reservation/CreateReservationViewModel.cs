using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Web.Models.Reservation
{
    public class CreateReservationViewModel
    {
        public int ReservationId { get; set; }

        public int ResourceId { get; set; }

        public string ResourceName { get; set; }

        public int CreatorId { get; set; }

        public string CreatorName { get; set; }

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
    }
}
