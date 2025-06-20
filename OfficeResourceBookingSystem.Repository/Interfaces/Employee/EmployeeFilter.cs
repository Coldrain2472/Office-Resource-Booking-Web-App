using System.Data.SqlTypes;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Employee
{
    public class EmployeeFilter
    {
        public SqlString? EmailAddress { get; set; }
    }
}
