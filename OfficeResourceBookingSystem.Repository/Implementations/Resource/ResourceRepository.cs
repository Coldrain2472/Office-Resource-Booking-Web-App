using Microsoft.Data.SqlClient;
using OfficeResourceBookingSystem.Repository.Base;
using OfficeResourceBookingSystem.Repository.Helpers;
using OfficeResourceBookingSystem.Repository.Interfaces.Resource;

namespace OfficeResourceBookingSystem.Repository.Implementations.Resource
{
    public class ResourceRepository : BaseRepository<Models.Resource>, IResourceRepository
    {
        private const string IdDbFieldEnumeratorName = "ResourceId";

        protected override string GetTableName()
        {
            return "Resources";
        }

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "Type",
            "Description",
            "IsAvailable"
        };

        protected override Models.Resource MapEntity(SqlDataReader reader)
        {
            return new Models.Resource
            {
                ResourceId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Type = Convert.ToString(reader["Type"]),
                Description = Convert.ToString(reader["Description"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
            };
        }

        public Task<int> CreateAsync(Models.Resource entity)
        {
            return base.CreateAsync(entity);
        }

        public Task<Models.Resource> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Resource> RetrieveCollectionAsync(ResourceFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }
            if (filter.Type is not null)
            {
                commandFilter.AddCondition("Type", filter.Type);
            }
            if (filter.IsAvailable is not null)
            {
                commandFilter.AddCondition("IsAvailable", filter.IsAvailable);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ResourceUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, "Resources", IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Type", update.Type);
            updateCommand.AddSetClause("IsAvailable", update.IsAvailable);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }
    }
}
