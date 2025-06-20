namespace OfficeResourceBookingSystem.Web.Models.Reservation
{
    public class EditReservationViewModel
    {
        public int ParticipantsCount { get; set; }

        public string Purpose { get; set; }

        public bool IsActive { get; set; }

        public DateTime EndTime { get; set; }

        public int ReservationId { get; set; }
    }
}
