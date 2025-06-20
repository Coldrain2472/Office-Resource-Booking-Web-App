using OfficeResourceBookingSystem.Repository.Base;

namespace OfficeResourceBookingSystem.Repository.Interfaces.Employee
{
    public interface IEmployeeRepository : IBaseRepository<Models.Employee, EmployeeFilter, EmployeeUpdate>
    {
    }
}
