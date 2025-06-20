using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Reservation
{
    public class ReservationFilter
    {
        public SqlInt32? ResourceId { get; set; }

        public SqlDateTime? StartTime { get; set; }

        public SqlBoolean? IsActive { get; set; }
    }
}
