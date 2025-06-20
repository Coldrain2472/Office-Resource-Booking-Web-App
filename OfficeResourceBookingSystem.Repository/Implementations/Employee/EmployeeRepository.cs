using Microsoft.Data.SqlClient;
using OfficeResourceBookingSystem.Repository.Base;
using OfficeResourceBookingSystem.Repository.Helpers;
using OfficeResourceBookingSystem.Repository.Interfaces.Employee;

namespace OfficeResourceBookingSystem.Repository.Implementations.Employee
{
    public class EmployeeRepository : BaseRepository<Models.Employee>, IEmployeeRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";

        protected override string GetTableName()
        {
            return "Employees";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "FullName",
            "EmailAddress",
            "Password"
        };

        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                FullName = Convert.ToString(reader["FullName"]),
                EmailAddress = Convert.ToString(reader["EmailAddress"]),
                Password = Convert.ToString(reader["Password"])
            };
        }

        public Task<int> CreateAsync(Models.Employee entity)
        {
            return base.CreateAsync(entity);
        }

        public Task<Models.Employee> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Employee> RetrieveCollectionAsync(EmployeeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.EmailAddress is not null)
            {
                commandFilter.AddCondition("EmailAddress", filter.EmailAddress);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Employees", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("FullName", update.FullName);
            updateCommand.AddSetClause("EmailAddress", update.EmailAddress);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}