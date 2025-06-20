using System.ComponentModel.DataAnnotations;

namespace OfficeResourceBookingSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Employee is required.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Resource is required.")]
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Purpose of the reservation is required.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Purpose must be between 3 and 150 characters.")]
        public string Purpose { get; set; }

        [Required(ErrorMessage = "Count of the participants is required.")]
        public int ParticipantsCount { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Creation date is required.")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt {  get; set; }
    }
}
