using OfficeResourceBookingSystem.Services.DTOs.Reservation;

namespace OfficeResourceBookingSystem.Services.Interfaces.Reservation
{
    public interface IReservationService
    {
        Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request);

        Task<GetReservationResponse> GetByIdAsync(int reservationId);

        Task<UpdateReservationResponse> UpdateAsync(int reservationId, UpdateReservationRequest request);

        Task<EndReservationResponse> EndReservationAsync(EndReservationRequest request);

        Task<GetReservationListResponse> GetActiveReservationsAsync();

        Task<GetReservationListResponse> GetInactiveReservationsAsync();

        Task<GetHistoryOfReservationsPerEmployeeResponse> GetHistoryOfReservationsPerEmployee(int employeeId); // История на резервациите по потребител

        Task<GetHistoryOfReservationsPerResourceResponse> GetHistoryOfReservationsPerResource(int resourceId); //  История на резервациите по ресурс
    }
}
