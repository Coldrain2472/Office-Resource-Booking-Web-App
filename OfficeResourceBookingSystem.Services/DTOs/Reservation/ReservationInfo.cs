namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class ReservationInfo
    {
        public int ReservationId { get; set; }

        public int CreatorId { get; set; } // EmployeeId

        public string? CreatedByName { get; set; }

        public int ResourceId { get; set; }

        public string? ResourceName { get; set; }

        public string Purpose { get; set; }

        public int ParticipantsCount { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; } // start date of reservation

        public DateTime EndTime { get; set; } // end date of reservation

        public DateTime CreatedAt { get; set; } // date of reservation creation
    }
}
