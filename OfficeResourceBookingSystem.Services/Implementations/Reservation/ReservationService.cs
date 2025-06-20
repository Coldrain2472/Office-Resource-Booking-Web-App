using OfficeResourceBookingSystem.Repository.Interfaces.Employee;
using OfficeResourceBookingSystem.Repository.Interfaces.Reservation;
using OfficeResourceBookingSystem.Repository.Interfaces.Resource;
using OfficeResourceBookingSystem.Services.DTOs.Reservation;
using OfficeResourceBookingSystem.Services.Interfaces.Reservation;
using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Services.Implementations.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IResourceRepository resourceRepository;
        private readonly IReservationRepository reservationRepository;

        public ReservationService(IEmployeeRepository _employeeRepository, IResourceRepository _resourceRepository, IReservationRepository _reservationRepository)
        {
            employeeRepository = _employeeRepository;
            resourceRepository = _resourceRepository;
            reservationRepository = _reservationRepository;
        }

        public async Task<GetReservationResponse> GetByIdAsync(int reservationId)
        {
            var reservation = await reservationRepository.RetrieveAsync(reservationId);
            return new GetReservationResponse
            {
                CreatedAt = reservation.CreatedAt,
                StartTime = reservation.StartTime,
                IsActive = reservation.IsActive,
                ParticipantsCount = reservation.ParticipantsCount,
                EndTime = reservation.EndTime,
                Purpose = reservation.Purpose,
                ResourceId = reservation.ResourceId,
                ReservationId = reservation.ReservationId,
                CreatorId = reservation.EmployeeId
            };
        }

        public async Task<GetReservationListResponse> GetInactiveReservationsAsync()
        {
            var filter = new ReservationFilter { IsActive = SqlBoolean.False };
            var reservations = await reservationRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var reservationsResponse = new GetReservationListResponse
            {
                Reservations = new List<ReservationInfo>()
            };


            foreach (var reservation in reservations)
            {
                if (reservation.EndTime < DateTime.Now)
                {
                    reservationsResponse.Reservations.Add(await MapToReservationInfoAsync(reservation));
                }
            }

            return reservationsResponse;
        }

        public async Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request)
        {
            if (request.StartTime <= DateTime.Now)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Start time must be in the future."
                };
            }

            var employee = await employeeRepository.RetrieveAsync(request.CreatorId);

            if (employee == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Employee not found."
                };
            }

            var resource = await resourceRepository.RetrieveAsync(request.ResourceId);

            if (resource == null)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Resource not found."
                };
            }

            // checking for overlapping active reservations
            var existingReservations = await reservationRepository.RetrieveCollectionAsync(new ReservationFilter
            {
                ResourceId = new SqlInt32(request.ResourceId),
                IsActive = new SqlBoolean(true)
            })
                .ToListAsync();

            bool overlaps = existingReservations.Any(r =>
                r.StartTime < request.EndTime && r.EndTime > request.StartTime);

            if (overlaps)
            {
                return new CreateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Resource is already reserved during this time."
                };
            }

            var reservation = new Models.Reservation
            {
                EmployeeId = request.CreatorId,
                ResourceId = request.ResourceId,
                Purpose = request.Purpose,
                ParticipantsCount = request.ParticipantsCount,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var reservationId = await reservationRepository.CreateAsync(reservation);

            return new CreateReservationResponse
            {
                Success = true,
                ReservationId = reservationId
            };
        }

        public async Task<UpdateReservationResponse> UpdateAsync(int reservationId, UpdateReservationRequest request)
        {
            var reservation = await reservationRepository.RetrieveAsync(reservationId);

            if (reservation == null || !reservation.IsActive)
            {
                return new UpdateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Active reservation not found."
                };
            }

            if (request.EndTime <= DateTime.Now)
            {
                return new UpdateReservationResponse
                {
                    Success = false,
                    ErrorMessage = "End time must be in the future."
                };
            }

            var update = new ReservationUpdate
            {
                Purpose = !string.IsNullOrEmpty(request.Purpose) ? new SqlString(request.Purpose) : SqlString.Null,
                ParticipantsCount = request.ParticipantsCount,
                EndTime = request.EndTime
            };

            var success = await reservationRepository.UpdateAsync(reservationId, update);

            return new UpdateReservationResponse
            {
                Success = true,
                ErrorMessage = success ? null : "Update failed."
            };
        }

        public async Task<GetHistoryOfReservationsPerEmployeeResponse> GetHistoryOfReservationsPerEmployee(int employeeId)
        {
            var reservationsPerEmployee = new List<ReservationInfo>();

            await foreach (var r in reservationRepository.RetrieveCollectionAsync(new ReservationFilter()))
            {
                if (r.EmployeeId == employeeId)
                {
                    reservationsPerEmployee.Add(await MapToReservationInfoAsync(r));
                }
            }

            return new GetHistoryOfReservationsPerEmployeeResponse
            {
                Reservations = reservationsPerEmployee,
                TotalCount = reservationsPerEmployee.Count
            };
        }

        public async Task<GetHistoryOfReservationsPerResourceResponse> GetHistoryOfReservationsPerResource(int resourceId)
        {
            var reservationsPerResource = new List<ReservationInfo>();

            await foreach (var r in reservationRepository.RetrieveCollectionAsync(new ReservationFilter()))
            {
                if (r.ResourceId == resourceId)
                {
                    reservationsPerResource.Add(new ReservationInfo
                    {
                        ReservationId = r.ReservationId,
                        ResourceId = r.ResourceId,
                        CreatorId = r.EmployeeId,
                        Purpose = r.Purpose,
                        ParticipantsCount = r.ParticipantsCount,
                        StartTime = r.StartTime,
                        EndTime = r.EndTime,
                        IsActive = r.IsActive,
                        CreatedAt = r.CreatedAt
                    });
                }
            }

            return new GetHistoryOfReservationsPerResourceResponse
            {
                Reservations = reservationsPerResource,
                TotalCount = reservationsPerResource.Count
            };
        }

        public async Task<GetReservationListResponse> GetActiveReservationsAsync()
        {
            var filter = new ReservationFilter { IsActive = SqlBoolean.True };

            var reservations = await reservationRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var reservationsResponse = new GetReservationListResponse
            {
                Reservations = new List<ReservationInfo>()
            };

            foreach (var reservation in reservations)
            {
                if (reservation.EndTime > DateTime.Now)
                {
                    reservationsResponse.Reservations.Add(await MapToReservationInfoAsync(reservation));
                }
            }

            return reservationsResponse;
        }

        public async Task<EndReservationResponse> EndReservationAsync(EndReservationRequest request)
        {
            var reservation = await reservationRepository.RetrieveAsync(request.ReservationId);

            if (reservation.EmployeeId != request.CreatedByEmployeeId)
            {
                return new EndReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Only the creator can end the reservation."
                };
            }

            var update = new ReservationUpdate
            {
                IsActive = SqlBoolean.False,
                EndTime = new SqlDateTime(DateTime.Now)
            };

            var isUpdated = await reservationRepository.UpdateAsync(request.ReservationId, update);

            if (isUpdated)
            {
                return new EndReservationResponse
                {
                    Success = true,
                    EndDate = (DateTime)update.EndTime
                };
            }
            else
            {
                return new EndReservationResponse
                {
                    Success = false,
                    ErrorMessage = "Unable to end the reservation."
                };
            }
        }

        private async Task<ReservationInfo> MapToReservationInfoAsync(Models.Reservation reservation)
        {
            var creator = await employeeRepository.RetrieveAsync(reservation.EmployeeId);
            var resource = await resourceRepository.RetrieveAsync(reservation.ResourceId);

            return new ReservationInfo
            {
                ReservationId = reservation.ReservationId,
                CreatorId = reservation.EmployeeId,
                ResourceId = reservation.ResourceId,
                StartTime = reservation.StartTime,
                EndTime = reservation.EndTime,
                CreatedAt = reservation.CreatedAt,
                ParticipantsCount = reservation.ParticipantsCount,
                Purpose = reservation.Purpose,
                CreatedByName = creator.FullName,
                ResourceName = resource.Name,
                IsActive = reservation.IsActive
            };
        }


    }
}