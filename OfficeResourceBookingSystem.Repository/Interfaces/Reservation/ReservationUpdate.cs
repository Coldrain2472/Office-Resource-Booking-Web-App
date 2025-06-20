using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Reservation
{
    public class ReservationUpdate
    {
        public SqlInt32? ParticipantsCount { get;set; }

        public SqlString? Purpose { get; set; }

        public SqlBoolean? IsActive { get; set; }

        public SqlDateTime? EndTime { get; set; }
    }
}
