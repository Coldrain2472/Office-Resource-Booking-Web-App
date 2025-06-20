using Microsoft.Data.SqlClient;
using OfficeResourceBookingSystem.Repository.Base;
using OfficeResourceBookingSystem.Repository.Helpers;
using OfficeResourceBookingSystem.Repository.Interfaces.Reservation;

namespace OfficeResourceBookingSystem.Repository.Implementations.Reservation
{
    public class ReservationRepository : BaseRepository<Models.Reservation>, IReservationRepository
    {
        private const string IdDbFieldEnumeratorName = "ReservationId";

        protected override string GetTableName()
        {
            return "Reservations";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "EmployeeId",
            "ResourceId",
            "Purpose",
            "ParticipantsCount",
            "StartTime",
            "EndTime",
            "IsActive",
            "CreatedAt"
        };

        protected override Models.Reservation MapEntity(SqlDataReader reader)
        {
            return new Models.Reservation
            {
                ReservationId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                EmployeeId = Convert.ToInt32(reader["EmployeeId"]),
                ResourceId = Convert.ToInt32(reader["ResourceId"]),
                Purpose = Convert.ToString(reader["Purpose"]),
                ParticipantsCount = Convert.ToInt32(reader["ParticipantsCount"]),
                StartTime = Convert.ToDateTime(reader["StartTime"]),
                EndTime = Convert.ToDateTime(reader["EndTime"]),
                IsActive = Convert.ToBoolean(reader["IsActive"]),
                CreatedAt = Convert.ToDateTime(reader["CreatedAt"]),
            };
        }

        public Task<int> CreateAsync(Models.Reservation entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.Reservation> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Reservation> RetrieveCollectionAsync(ReservationFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.StartTime is not null)
            {
                commandFilter.AddCondition("StartTime", filter.StartTime);
            }
            if (filter.IsActive is not null)
            {
                commandFilter.AddCondition("IsActive", filter.IsActive);
            }
            if (filter.ResourceId is not null)
            {
                commandFilter.AddCondition("ResourceId", filter.ResourceId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ReservationUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Reservations", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("ParticipantsCount", update.ParticipantsCount);
            updateCommand.AddSetClause("EndTime", update.EndTime);
            updateCommand.AddSetClause("IsActive", update.IsActive);
            updateCommand.AddSetClause("Purpose", update.Purpose);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }
    }
}
