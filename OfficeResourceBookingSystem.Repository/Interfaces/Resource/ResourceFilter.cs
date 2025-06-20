using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Resource
{
    public class ResourceFilter
    {
        public SqlString? Name { get; set; }

        public SqlString? Type { get; set; }

        public SqlBoolean? IsAvailable { get; set; }
    }
}
