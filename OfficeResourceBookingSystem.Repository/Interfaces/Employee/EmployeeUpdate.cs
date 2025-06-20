using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Employee
{
    public class EmployeeUpdate
    {
        public SqlString? FullName { get; set; }

        public SqlString? EmailAddress { get; set; }
    }
}
