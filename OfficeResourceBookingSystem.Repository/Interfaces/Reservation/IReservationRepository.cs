using OfficeResourceBookingSystem.Repository.Base;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Reservation
{
    public interface IReservationRepository : IBaseRepository<Models.Reservation, ReservationFilter, ReservationUpdate>
    {
    }
}
