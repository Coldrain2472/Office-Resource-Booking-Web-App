namespace OfficeResourceBookingSystem.Services.DTOs.Reservation
{
    public class UpdateReservationRequest
    {
        public int ParticipantsCount { get; set; }

        public DateTime EndTime { get; set; }

        public string Purpose { get; set; }

        public bool IsActive { get; set; }
    }
}
